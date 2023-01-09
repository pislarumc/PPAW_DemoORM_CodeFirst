using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_01.Models;
using NivelAccesDate_ORM_CodeFirst.Models;

namespace MVC_01.Controllers
{
    public class ImageController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Image
        public ActionResult Index()
        {
            List<Image> images = db.Images.Include(img => img.User).ToList();
            List<ImageViewModel> imageViewModels = new List<ImageViewModel>();
            foreach (Image image in images)
            {
                ImageViewModel imageViewModel = new ImageViewModel()
                {
                    ImageId = image.ImageId,
                    Name = image.Name,
                    Path = image.Path,
                    Dimensions = image.Dimensions,
                    User = image.User != null ? new UserViewModel()
                    {
                        UserName = image.User.UserName,
                        Email = image.User.Email,
                        Password = image.User.Password,
                        UserId = image.User.UserId,
                        Role = image.User.Role,
                        Subscribed = image.User.Subscribed
                    }:null
                };

                imageViewModels.Add(imageViewModel);
            }
            return View(imageViewModels);
        }

        // GET: Image/Create
        public ActionResult Create()
        {
            ImageViewModel imageViewModel = new ImageViewModel();
            //imageViewModel.Users = db.Users.Select(u => u.User).ToList() as IEnumerable<SelectListItem>;
            //imageViewModel.Users = db.Users.Select(u => u.User).ToList();
            imageViewModel.Users = db.Users.Select(u => u.UserId).ToList().Select(u=>
                                                                                        new SelectListItem()
                                                                                        {
                                                                                            Text=u.ToString(),
                                                                                            Value = u
                                                                                        });
            return View(imageViewModel);
        }

        // POST: Image/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ImageViewModel imageViewModel)
        {
            try
            {
                imageViewModel.Users = db.Users.Select(u => u.UserId).ToList().Select(u =>
                                                                                            new SelectListItem()
                                                                                            {
                                                                                                Text = u.ToString(),
                                                                                                Value = u
                                                                                            });
                User Utilizator = (User)db.Users.Where(u => u.UserId == imageViewModel.UserIDFK).FirstOrDefault();
                if (ModelState.IsValid)
                {
                    Image image = new Image()
                    {
                        ImageId=imageViewModel.ImageId,
                        Name = imageViewModel.Name,
                        Path = imageViewModel.Path,
                        Dimensions = imageViewModel.Dimensions,
                        User = (User)db.Users.Where(u => u.UserId == imageViewModel.UserIDFK).FirstOrDefault()
                    };

                    db.Images.Add(image);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                    return View(imageViewModel);
            }
            catch
            {
                return View(imageViewModel);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
