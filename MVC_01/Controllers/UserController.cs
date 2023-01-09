using MVC_01.Models;
using NivelAccesDate_ORM_CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVC_01.Controllers
{
    public class UserController : Controller
    {
        public DatabaseContext db = new DatabaseContext();
        // GET: User
        public ActionResult Index()
        {
            List<User> users = db.Users.ToList();
            List<UserViewModel> userViewModels = new List<UserViewModel>();
            foreach (User user in users)
            {
                UserViewModel userViewModel = new UserViewModel()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password,
                    UserId = user.UserId,
                    Role = user.Role,
                    Subscribed = user.Subscribed
                };

                userViewModels.Add(userViewModel);
            }
            return View(userViewModels);
        }

        // GET: User/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            UserViewModel userViewModel = new UserViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                UserId = user.UserId,
                Role = user.Role,
                Subscribed = user.Subscribed
            };

            return View(userViewModel);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            UserViewModel userViewModel = new UserViewModel();
            return View(userViewModel);
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(UserViewModel userViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = new User()
                    {
                        UserName = userViewModel.UserName,
                        Email = userViewModel.Email,
                        Password = userViewModel.Password,
                        UserId = userViewModel.UserId,
                        Role = userViewModel.Role,
                        Subscribed = userViewModel.Subscribed
                    };

                    db.Users.Add(user);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                    return View(userViewModel);
            }
            catch
            {
                return View(userViewModel);
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            UserViewModel userViewModel = new UserViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                UserId = user.UserId,
                Role = user.Role,
                Subscribed = user.Subscribed
            };

            return View(userViewModel);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "UserName,Email,Password,User,Role,Subscribed")] UserViewModel userViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = new User()
                    {
                        UserName = userViewModel.UserName,
                        Email = userViewModel.Email,
                        Password = userViewModel.Password,
                        UserId = userViewModel.UserId,
                        Role = userViewModel.Role,
                        Subscribed = userViewModel.Subscribed
                    };

                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                    return View(userViewModel);
            }
            catch
            {
                return View(userViewModel);
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
