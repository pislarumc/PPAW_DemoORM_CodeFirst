using Business_Layer.CoreService.Interfaces;
using NivelAccesDate_ORM_CodeFirst.Interfaces;
using NivelAccesDate_ORM_CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Layer.Interfaces;
using NLog;

namespace Business_Layer
{
    public class HistoriesService:IHistories
    {
        protected Logger logger = LogManager.GetCurrentClassLogger();

        private DatabaseContext db;

        private ICache cacheManager;

        public HistoriesService(IDatabaseContext db, ICache cacheManager)
        {
            this.db = (DatabaseContext)db;
            this.cacheManager = cacheManager;
        }
        public List<History> GetHistories()
        {
            string key = "history_list_all";
            List<History> histories;

            if (cacheManager.IsSet(key))
            {
                histories = cacheManager.Get<List<History>>(key);
            }
            else
            {
                try
                {
                    histories = db.Histories.Include(his => his.User).Include(his => his.Image).Include(his => his.Effect).Include(his => his.Image.User).ToList();
                    cacheManager.Set(key, histories);
                }
                catch(Exception exception)
                {
                    logger.Error(exception, "Eroare la preluare istorice din db");
                    histories = null;
                }
            }

            return histories;
        }
        public History GetHistory(string id)
        {
            string key = "history_" + id;
            History history;
            if (cacheManager.IsSet(key))

            {
                history = cacheManager.Get<History>(key);
            }
            else
            {
                try
                {
                    history = db.Histories.Include(his => his.User).Include(his => his.Image).Include(his => his.Effect).Include(his => his.Image.User).FirstOrDefault(his => his.HistoryId == id);
                    cacheManager.Set(key, history);
                }
                catch(Exception exception)
                {
                    logger.Error(exception, "Eroare la preluare istoric din db");
                    history = null;
                }
            }
            return history;
        }
        public bool AddHistory(History historyForDB)
        {
            try
            {
                db.Histories.Add(historyForDB);
                db.SaveChanges();
                cacheManager.Remove("history_list_all");
                return true;
            }
            catch(Exception exception)
            {
                logger.Error(exception, "Eroare la adaugare istoric in db");
                return false;
            }
        }
        public bool UpdateHistory(string id, History historyForDB)
        {
            db.Entry(historyForDB).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                //remove from cache
                string individual_key = "history_" + id;
                string list_key = "history_list";
                cacheManager.Remove(individual_key);
                cacheManager.RemoveByPattern(list_key);
                //get back data to cache
                cacheManager.Set(individual_key, historyForDB);
            }
            catch (DbUpdateConcurrencyException)
            {
                logger.Error( "Eroare la editare istoric din db");
                if (db.Histories.Count(e => e.HistoryId == id) == 0)
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }
        public bool DeleteHistory(string id)
        {
            try
            {
                History HistoryToDelete = db.Histories.Find(id);
                History HistoryDeleted;
                if (HistoryToDelete != null)
                {
                    HistoryDeleted = db.Histories.Remove(HistoryToDelete);
                    db.SaveChanges();
                    string individual_key = "history_" + id;
                    string list_key = "history_list";
                    cacheManager.Remove(individual_key);
                    cacheManager.RemoveByPattern(list_key);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception exception) 
            {
                logger.Error(exception, "Eroare la stergere istoric din db");
                return false;
            }

        }

        public bool HistoryExists(string id)
        {
            return db.Histories.Count(e => e.HistoryId == id) > 0;
        }

        public static History AdaugareIstoric(string descriere, string efect, string user, string imagine)
        {
            Random rnd = new Random();
            History historyForDB = new History
            {
                LoggingTime = DateTime.UtcNow,
                HistoryId = rnd.Next(1000, 10000) + "_" + DateTime.UtcNow,
                Description = descriere,
                EffectId = efect,
                UserId = user,
                ImageId = imagine

            };
            return historyForDB;
        }


        // method injection
        public void SetDependency(IDatabaseContext db)
        {
            this.db = (DatabaseContext)db;
        }

        //property injection
        public IDatabaseContext DbContextProperty
        {
            set
            {
                this.db = (DatabaseContext)value;
            }
            get
            {
                if (db == null)
                {
                    throw new Exception("DbContext is not initialized");
                }
                else
                {
                    return db;
                }
            }
        }
    }
}
