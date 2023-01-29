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
using System.Runtime.Remoting.Contexts;
using NLog;

namespace Business_Layer
{
    public class ImagesService:IImages
    {

        protected Logger logger = LogManager.GetCurrentClassLogger();

        private DatabaseContext db;

        private ICache cacheManager;

        public ImagesService(IDatabaseContext db, ICache cacheManager)
        {
            this.db = (DatabaseContext)db;
            this.cacheManager = cacheManager;
        }
        public List<Image> GetImages()
        {
            string key = "image_list_all";
            List<Image> images;

            if (cacheManager.IsSet(key))
            {
                images = cacheManager.Get<List<Image>>(key);
            }
            else
            {
                try
                {
                    images = db.Images.Include(img => img.User).ToList();
                    cacheManager.Set(key, images);
                }
                catch(Exception exception)
                {
                    logger.Error(exception,"Eroare la preluare imagini din db");
                    images = null;
                }
            }
            return images;
        }

        public List<string> GetImageUsers()
        {
            List<string> usersId;
            try
            {
                usersId = db.Users.Where(user => user.Deleted == false).Select(user => user.UserId).ToList();
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Eroare la preluare imagine din db");
                usersId = null;
            }
            return usersId;
        }

        public Image GetImage(string id)
        {
            string key = "image_" + id;
            Image image;
            if (cacheManager.IsSet(key))

            {
                image = cacheManager.Get<Image>(key);
            }
            else
            {
                image = db.Images.Include(img => img.User).FirstOrDefault(img => img.ImageId == id);
                cacheManager.Set(key, image);
            }

            return image;
        }
        public bool AddImage(Image imageForDB)
        {
            try
            {
                db.Images.Add(imageForDB);
                db.SaveChanges();
                cacheManager.Remove("image_list_all");

                History historyForDB = HistoriesService.AdaugareIstoric("Adaugare imagine: ", null, null, imageForDB.ImageId);
                db.Histories.Add(historyForDB);
                db.SaveChanges();

                return true;
            }
            catch(Exception exception)
            {
                logger.Error(exception, "Eroare la creare iamgine");
                return false;
            }
        }
        public bool UpdateImage(string id, Image imageForDB)
        {
            imageForDB.User = null;//numaidecat valori nule in obiect
            db.Entry(imageForDB).State = EntityState.Modified;

            //----------------------------------------------------------------------------------------------------------------------------------
            string changed_properties = "";
            int nr_changed_properties = 0;

            var change = db.Entry(imageForDB);
                var entityName = change.Entity.GetType().Name;
                foreach (var prop in change.OriginalValues.PropertyNames)
                {
                    var originalValue = change.GetDatabaseValues()[prop]?.ToString();
                    var currentValue = change.CurrentValues[prop]?.ToString();
                    if (originalValue != currentValue)
                    {
                        changed_properties += prop+", ";
                        nr_changed_properties ++;
                    }
                }
            //----------------------------------------------------------------------------------------------------------------------------------

            try
            {
                db.SaveChanges();
                //remove from cache
                string individual_key = "image_" + id;
                string list_key = "image_list";
                cacheManager.Remove(individual_key);
                cacheManager.RemoveByPattern(list_key);
                //get back data to cache
                cacheManager.Set(individual_key, imageForDB);

                //adaugare  istoric
                History historyForDB = HistoriesService.AdaugareIstoric("Modificare imagine[ "+ nr_changed_properties + " ]: "+ changed_properties, null, null, imageForDB.ImageId);
                db.Histories.Add(historyForDB);
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                logger.Error("Eroare la actualizare imagine din db");
                if (db.Images.Count(e => e.ImageId == id) == 0)
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
        public bool DeleteToggleImage(string id, bool deleted)
        {
            try
            {
                Image ImageToDelete = db.Images.Find(id);
                Image ImageDeleted;
                if (ImageToDelete != null)
                {
                    //ImageDeleted = db.Images.Remove(ImageToDelete);
                    //db.SaveChanges();

                    //stergere logica
                    ImageToDelete.Deleted = deleted;
                    db.Images.Attach(ImageToDelete);
                    db.Entry(ImageToDelete).Property(x => x.Deleted).IsModified = true;
                    db.SaveChanges();

                    string individual_key = "image_" + id;
                    string list_key = "image_list";
                    cacheManager.Remove(individual_key);
                    cacheManager.RemoveByPattern(list_key);

                    //adaugare date in istoric
                    History historyForDB = HistoriesService.AdaugareIstoric("Stergere imagine", null, null, id);
                    db.Histories.Add(historyForDB);
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception exception) {
                logger.Error(exception, "Eroare la stergere iamgine");
                return false; 
            }

        }

        public bool ImageExists(string id)
        {
            return db.Images.Count(e => e.ImageId == id) > 0;
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
