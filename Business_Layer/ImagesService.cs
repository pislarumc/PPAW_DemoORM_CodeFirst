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
    public class ImagesService:IImages
    {
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
                images = db.Images.Include(img => img.User).ToList();
                cacheManager.Set(key, images);
            }

            return images;
        }

        public List<string> GetImageUsers()
        {
            List<string> usersId;

            usersId = db.Users.Where(user => user.Deleted == false).Select(user => user.UserId).ToList();

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
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateImage(string id, Image imageForDB)
        {
            if (imageForDB.User != null)
            {
                User user = db.Users.Find(imageForDB.User.UserId);
                imageForDB.User = user;
                db.Entry(user).State = EntityState.Modified;
            }
            //    imageForDB.User = db.Users.Find(imageForDB.User.User);
            imageForDB.User = null;//numaidecat valori nule in obiect
            db.Entry(imageForDB).State = EntityState.Modified;
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
            }
            catch (DbUpdateConcurrencyException)
            {
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
                }
                return true;
            }
            catch (Exception err) { return false; }

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
