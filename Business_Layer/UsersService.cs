using Business_Layer.CoreService.Interfaces;
using Business_Layer.Interfaces;
using NivelAccesDate_ORM_CodeFirst.Interfaces;
using NivelAccesDate_ORM_CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Business_Layer
{
    public class UsersService : IUsers
    {
        //public DatabaseContext db = new DatabaseContext();
        private DatabaseContext db;

        private ICache cacheManager;

        //public UsersService()
        //{
        //}
        public UsersService(IDatabaseContext db, ICache cacheManager)
        {
            this.db = (DatabaseContext)db;
            this.cacheManager = cacheManager;
        }
        public List<User> GetUsers()
        {
            string key = "user_list_all";
            List<User> users;

            if (cacheManager.IsSet(key))
            {
                users = cacheManager.Get<List<User>>(key);
            }
            else
            {
                users = db.Users.ToList();
                cacheManager.Set(key, users);
            }

            return users;
        }
        public User GetUser(string id)
        {
            string key = "user_" + id;
            User user;
            if (cacheManager.IsSet(key))

            {
                user = cacheManager.Get<User>(key);
            }
            else
            {
                user = db.Users.Find(id);
                cacheManager.Set(key, user);
            }
            return user;
        }
        public bool AddUser(User userForDB)
        {
            try
            {
                db.Users.Add(userForDB);
                db.SaveChanges();
                cacheManager.Remove("user_list_all");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateUser(string id,User userForDB)
        {
            db.Entry(userForDB).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                //remove from cache
                string individual_key = "user_" + id;
                string list_key = "user_list";
                cacheManager.Remove(individual_key);
                cacheManager.RemoveByPattern(list_key);
                //get back data to cache
                cacheManager.Set(individual_key, userForDB);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Users.Count(e => e.UserId == id) == 0)
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
        public bool DeleteToggleUser(string id, bool deleted)
        {
            try
            {
                User UserToDelete = db.Users.Find(id);
                User UserDeleted;
                if (UserToDelete != null)
                {
                    //UserDeleted = db.Users.Remove(UserToDelete);
                    //db.SaveChanges();

                    //stergere logica
                    UserToDelete.Deleted = deleted;
                    db.Users.Attach(UserToDelete);
                    db.Entry(UserToDelete).Property(x => x.Deleted).IsModified = true;
                    db.SaveChanges();

                    string individual_key = "user_" + id;
                    string list_key = "user_list";
                    cacheManager.Remove(individual_key);
                    cacheManager.RemoveByPattern(list_key);
                }
                return true;
            }
            catch(Exception err) { return false; }

        }

        private bool UserExists(string id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
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
