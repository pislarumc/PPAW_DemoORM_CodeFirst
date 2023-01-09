using NivelAccesDate_ORM_CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Web.Http.Description;
using Business_Layer;
using Business_Layer.Interfaces;
using System.Drawing.Text;
using System.Web.Http.Cors;
//for cors
//using System.Web.Http.Cors;

namespace API_02.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<NivelAccesDate_ORM_CodeFirst.Models.User, API_02.Models.UserModel>();
            cfg.CreateMap<API_02.Models.UserModel, NivelAccesDate_ORM_CodeFirst.Models.User>();
        });
        //public DatabaseContext db = new DatabaseContext();

        //UsersService UsersServiceLocal = new UsersService();
        private IUsers UsersServiceLocal;
        public UserController(IUsers UsersServiceLocal)
        {
            this.UsersServiceLocal = UsersServiceLocal;
        }

        // GET: api/UserModel
        public List<API_02.Models.UserModel> Get()
        {
            //List<UserModel> users = db.Users.ToList();
            //return users;
            //List<NivelAccesDate_ORM_CodeFirst.Models.UserModel> usersFromDB = db.Users.ToList();
            List<NivelAccesDate_ORM_CodeFirst.Models.User> usersFromDB  = UsersServiceLocal.GetUsers();
            Mapper mapper = new Mapper(config);
            List<API_02.Models.UserModel> users = mapper
                .Map<List<NivelAccesDate_ORM_CodeFirst.Models.User>, List<API_02.Models.UserModel>>(usersFromDB);

            return users;
        }

        // GET: api/UserModel/5
        public IHttpActionResult Get(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            //NivelAccesDate_ORM_CodeFirst.Models.UserModel userFromDB = db.Users.Find(id);
            NivelAccesDate_ORM_CodeFirst.Models.User userFromDB = UsersServiceLocal.GetUser(id);
            if (userFromDB == null)
            {
                return NotFound();
            }
            Mapper mapper = new Mapper(config);
            API_02.Models.UserModel user = mapper
                .Map<NivelAccesDate_ORM_CodeFirst.Models.User, API_02.Models.UserModel>(userFromDB);
            return Ok(user);
        }

        // POST: api/UserModel
        [ResponseType(typeof(API_02.Models.UserModel))]
        public IHttpActionResult Post(API_02.Models.UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Mapper mapper = new Mapper(config);
            NivelAccesDate_ORM_CodeFirst.Models.User userForDB = mapper
                .Map< API_02.Models.UserModel, NivelAccesDate_ORM_CodeFirst.Models.User>(user);
            //db.Users.Add(userForDB);
            //db.SaveChanges();
            UsersServiceLocal.AddUser(userForDB);

            return CreatedAtRoute("DefaultApi", new { id = userForDB.UserId}, userForDB);
        }

        // PUT: api/UserModel/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(string id, API_02.Models.UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (user is null || id != user.UserId)
            {
                return BadRequest(ModelState);
            }

            Mapper mapper = new Mapper(config);
            NivelAccesDate_ORM_CodeFirst.Models.User userForDB = mapper
                .Map<API_02.Models.UserModel, NivelAccesDate_ORM_CodeFirst.Models.User>(user);

            //db.Entry(userForDB).State = EntityState.Modified;

            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (db.Users.Count(e => e.User == id) == 0)
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            if(UsersServiceLocal.UpdateUser(id,userForDB)==false)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // DELETE: api/UserModel/5
        public IHttpActionResult Delete(string id)
        {
            if (UsersServiceLocal.DeleteToggleUser(id, true) == true)
                return Ok(id);
            else
                return BadRequest(id);
        }
    }
}
