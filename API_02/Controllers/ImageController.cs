using AutoMapper;
using Business_Layer.Interfaces;
using NivelAccesDate_ORM_CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API_02.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ImageController : ApiController
    {
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<NivelAccesDate_ORM_CodeFirst.Models.Image, API_02.Models.ImageModel>();
            cfg.CreateMap<API_02.Models.ImageModel, NivelAccesDate_ORM_CodeFirst.Models.Image>();
        });
        MapperConfiguration config_user = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<NivelAccesDate_ORM_CodeFirst.Models.User, API_02.Models.UserModel>();
            cfg.CreateMap<API_02.Models.UserModel, NivelAccesDate_ORM_CodeFirst.Models.User>();
        });

        private IImages ImagesServiceLocal;
        public ImageController(IImages ImagesServiceLocal)
        {
            this.ImagesServiceLocal = ImagesServiceLocal;
        }
        // GET: api/Image
        public IEnumerable<API_02.Models.ImageModel> Get()
        {
            List<NivelAccesDate_ORM_CodeFirst.Models.Image> imagesFromDB = ImagesServiceLocal.GetImages();
            Mapper mapper = new Mapper(config);
            Mapper mapper_user = new Mapper(config_user);

            //List<API_02.Models.ImageModel> images = mapper
            //    .Map<List<NivelAccesDate_ORM_CodeFirst.Models.Image>, List<API_02.Models.ImageModel>>(imagesFromDB);

            //lista de useri din modelul de imagini
            List<string> image_users = ImagesServiceLocal.GetImageUsers();

            List<API_02.Models.ImageModel> images = new List<API_02.Models.ImageModel>();
            API_02.Models.ImageModel image;
            foreach (NivelAccesDate_ORM_CodeFirst.Models.Image imageFromDB in imagesFromDB)
            {
                image = mapper.Map<NivelAccesDate_ORM_CodeFirst.Models.Image, API_02.Models.ImageModel>(imageFromDB);
                image.UserModel = mapper_user.Map< NivelAccesDate_ORM_CodeFirst.Models.User, API_02.Models.UserModel > (imageFromDB.User);
                //delete passwords
                if(image.UserModel != null)
                    image.UserModel.Password = null;
                images.Add(image);
            }

            //memebru static
            //API_02.Models.ImageModel.Users_list = image_users;

            images.First().Users_list = image_users;

            return images;
        }

        // GET: api/Image/5
        public IHttpActionResult Get(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            NivelAccesDate_ORM_CodeFirst.Models.Image imageFromDB = ImagesServiceLocal.GetImage(id);
            if (imageFromDB == null)
            {
                return NotFound();
            }
            Mapper mapper = new Mapper(config);
            Mapper mapper_user = new Mapper(config_user);
            API_02.Models.ImageModel image = mapper
                .Map<NivelAccesDate_ORM_CodeFirst.Models.Image, API_02.Models.ImageModel>(imageFromDB);
            image.UserModel = mapper_user.Map<NivelAccesDate_ORM_CodeFirst.Models.User, API_02.Models.UserModel>(imageFromDB.User);

            //delete passwords
            image.UserModel.Password = null;
            return Ok(image);
        }

        // POST: api/Image
        public IHttpActionResult Post(API_02.Models.ImageModel image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Mapper mapper = new Mapper(config);
            NivelAccesDate_ORM_CodeFirst.Models.Image imageForDB = mapper
                .Map<API_02.Models.ImageModel, NivelAccesDate_ORM_CodeFirst.Models.Image>(image);
            if (ImagesServiceLocal.AddImage(imageForDB) == false)
                return InternalServerError();

            //return CreatedAtRoute("DefaultApi", new { id = imageForDB.ImageId }, imageForDB);
            return CreatedAtRoute("DefaultApi", new { id = imageForDB.ImageId }, imageForDB);
        }

        // PUT: api/Image/5
        public IHttpActionResult Put(string id, API_02.Models.ImageModel image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (image is null || id != image.ImageId)
            {
                return BadRequest(ModelState);
            }

            Mapper mapper = new Mapper(config);
            NivelAccesDate_ORM_CodeFirst.Models.Image imageForDB = mapper
                .Map<API_02.Models.ImageModel, NivelAccesDate_ORM_CodeFirst.Models.Image>(image);
            if (ImagesServiceLocal.UpdateImage(id, imageForDB) == false)
            {
                return InternalServerError();
            }

            return Ok(image);
        }

        // DELETE: api/Image/5
        public IHttpActionResult Delete(string id)
        {
            if (ImagesServiceLocal.DeleteToggleImage(id, true) == true)
                return Ok(id);
            else
                return InternalServerError();
        }
    }
}
