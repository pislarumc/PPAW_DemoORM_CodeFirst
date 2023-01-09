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

namespace Business_Layer
{
    public class EffectsService:IEffects
    {
        private DatabaseContext db;

        private ICache cacheManager;

        public EffectsService(IDatabaseContext db, ICache cacheManager)
        {
            this.db = (DatabaseContext)db;
            this.cacheManager = cacheManager;
        }
        public List<Effect> GetEffects()
        {
            string key = "effect_list_all";
            List<Effect> effects;

            if (cacheManager.IsSet(key))
            {
                effects = cacheManager.Get<List<Effect>>(key);
            }
            else
            {
                effects = db.Effects.ToList();
                cacheManager.Set(key, effects);
            }

            return effects;
        }
        public Effect GetEffect(string id)
        {
            string key = "effect_" + id;
            Effect effect;
            if (cacheManager.IsSet(key))

            {
                effect = cacheManager.Get<Effect>(key);
            }
            else
            {
                effect = db.Effects.Find(id);
                cacheManager.Set(key, effect);
            }
            return effect;
        }
        public bool AddEffect(Effect effectForDB)
        {
            try
            {
                db.Effects.Add(effectForDB);
                db.SaveChanges();
                cacheManager.Remove("effect_list_all");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateEffect(string id, Effect effectForDB)
        {
            db.Entry(effectForDB).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                //remove from cache
                string individual_key = "effect_" + id;
                string list_key = "effect_list";
                cacheManager.Remove(individual_key);
                cacheManager.RemoveByPattern(list_key);
                //get back data to cache
                cacheManager.Set(individual_key, effectForDB);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Effects.Count(e => e.EffectId == id) == 0)
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
        public bool DeleteToggleEffect(string id, bool deleted)
        {
            try
            {
                Effect EffectToDelete = db.Effects.Find(id);
                Effect EffectDeleted;
                if (EffectToDelete != null)
                {
                    //EffectDeleted = db.Effects.Remove(EffectToDelete);
                    //db.SaveChanges();

                    //stergere logica
                    EffectToDelete.Deleted = deleted;
                    db.Effects.Attach(EffectToDelete);
                    db.Entry(EffectToDelete).Property(x => x.Deleted).IsModified = true;
                    db.SaveChanges();

                    string individual_key = "effect_" + id;
                    string list_key = "effect_list";
                    cacheManager.Remove(individual_key);
                    cacheManager.RemoveByPattern(list_key);
                }
                return true;
            }
            catch (Exception err) { return false; }

        }

        private bool EffectExists(string id)
        {
            return db.Effects.Count(e => e.EffectId == id) > 0;
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
