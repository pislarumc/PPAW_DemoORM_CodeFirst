using AutoMapper;
using Business_Layer.Interfaces;
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
    public class HistoryController : ApiController
    {
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<NivelAccesDate_ORM_CodeFirst.Models.History, API_02.Models.HistoryModel>();
            cfg.CreateMap<API_02.Models.HistoryModel, NivelAccesDate_ORM_CodeFirst.Models.History>();
        });
        MapperConfiguration config_image = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<NivelAccesDate_ORM_CodeFirst.Models.Image, API_02.Models.ImageModel>();
            cfg.CreateMap<API_02.Models.ImageModel, NivelAccesDate_ORM_CodeFirst.Models.Image>();
        });
        MapperConfiguration config_user = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<NivelAccesDate_ORM_CodeFirst.Models.User, API_02.Models.UserModel>();
            cfg.CreateMap<API_02.Models.UserModel, NivelAccesDate_ORM_CodeFirst.Models.User>();
        });
        MapperConfiguration config_effect = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<NivelAccesDate_ORM_CodeFirst.Models.Effect, API_02.Models.EffectModel>();
            cfg.CreateMap<API_02.Models.EffectModel, NivelAccesDate_ORM_CodeFirst.Models.Effect>();
        });


        private IHistories HistoriesServiceLocal;
        public HistoryController(IHistories HistoriesServiceLocal)
        {
            this.HistoriesServiceLocal = HistoriesServiceLocal;
        }
        // GET: api/History
        public IEnumerable<API_02.Models.HistoryModel> Get()
        {
            List<NivelAccesDate_ORM_CodeFirst.Models.History> historiesFromDB = HistoriesServiceLocal.GetHistories();
            Mapper mapper = new Mapper(config);
            //List<API_02.Models.HistoryModel> histories = mapper
            //    .Map<List<NivelAccesDate_ORM_CodeFirst.Models.History>, List<API_02.Models.HistoryModel>>(historiesFromDB);

            //mapping nested objects
            Mapper mapper_user = new Mapper(config_user);
            Mapper mapper_image = new Mapper(config_image);
            Mapper mapper_effect = new Mapper(config_effect);
            List<API_02.Models.HistoryModel> histories = new List<API_02.Models.HistoryModel>();
            API_02.Models.HistoryModel history;
            foreach (NivelAccesDate_ORM_CodeFirst.Models.History historyFromDB in historiesFromDB)
            {
                history = mapper.Map<NivelAccesDate_ORM_CodeFirst.Models.History, API_02.Models.HistoryModel>(historyFromDB);
                history.UserModel = mapper_user.Map<NivelAccesDate_ORM_CodeFirst.Models.User, API_02.Models.UserModel>(historyFromDB.User);
                history.EffectModel = mapper_effect.Map<NivelAccesDate_ORM_CodeFirst.Models.Effect, API_02.Models.EffectModel>(historyFromDB.Effect);
                history.ImageModel = mapper_image.Map<NivelAccesDate_ORM_CodeFirst.Models.Image, API_02.Models.ImageModel>(historyFromDB.Image);
                history.ImageModel.UserModel = mapper_user.Map<NivelAccesDate_ORM_CodeFirst.Models.User, API_02.Models.UserModel>(historyFromDB.Image.User);
                //delete passwords
                history.UserModel.Password = null;
                history.ImageModel.UserModel.Password = null;
                histories.Add(history);
            }

            return histories;
        }

        // GET: api/History/5
        public IHttpActionResult Get(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            NivelAccesDate_ORM_CodeFirst.Models.History historyFromDB = HistoriesServiceLocal.GetHistory(id);
            if (historyFromDB == null)
            {
                return NotFound();
            }
            Mapper mapper = new Mapper(config);
            //API_02.Models.HistoryModel history = mapper
            //    .Map<NivelAccesDate_ORM_CodeFirst.Models.History, API_02.Models.HistoryModel>(historyFromDB);
            Mapper mapper_user = new Mapper(config_user);
            Mapper mapper_image = new Mapper(config_image);
            Mapper mapper_effect = new Mapper(config_effect);
            API_02.Models.HistoryModel history = mapper
                .Map<NivelAccesDate_ORM_CodeFirst.Models.History, API_02.Models.HistoryModel>(historyFromDB);
            history.UserModel = mapper_user.Map<NivelAccesDate_ORM_CodeFirst.Models.User, API_02.Models.UserModel>(historyFromDB.User);
            history.EffectModel = mapper_effect.Map<NivelAccesDate_ORM_CodeFirst.Models.Effect, API_02.Models.EffectModel>(historyFromDB.Effect);
            history.ImageModel = mapper_image.Map<NivelAccesDate_ORM_CodeFirst.Models.Image, API_02.Models.ImageModel>(historyFromDB.Image);
            history.ImageModel.UserModel = mapper_user.Map<NivelAccesDate_ORM_CodeFirst.Models.User, API_02.Models.UserModel>(historyFromDB.Image.User);
            //delete passwords
            history.UserModel.Password = null;
            history.ImageModel.UserModel.Password = null;
            return Ok(history);
        }

        // POST: api/History
        public IHttpActionResult Post(API_02.Models.HistoryModel history)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Mapper mapper = new Mapper(config);
            NivelAccesDate_ORM_CodeFirst.Models.History historyForDB = mapper
                .Map<API_02.Models.HistoryModel, NivelAccesDate_ORM_CodeFirst.Models.History>(history);
            if(HistoriesServiceLocal.AddHistory(historyForDB) == false)
                return InternalServerError();

            return CreatedAtRoute("DefaultApi", new { id = historyForDB.HistoryId }, historyForDB);
        }

        // PUT: api/History/5
        public IHttpActionResult Put(string id, API_02.Models.HistoryModel history)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (history is null || id != history.HistoryId)
            {
                return BadRequest(ModelState);
            }

            Mapper mapper = new Mapper(config);
            NivelAccesDate_ORM_CodeFirst.Models.History historyForDB = mapper
                .Map<API_02.Models.HistoryModel, NivelAccesDate_ORM_CodeFirst.Models.History>(history);
            if (HistoriesServiceLocal.UpdateHistory(id, historyForDB) == false)
            {
                return InternalServerError();
            }

            return Ok(history);
        }

        // DELETE: api/History/5
        public IHttpActionResult Delete(string id)
        {
            if (HistoriesServiceLocal.DeleteHistory(id) == true)
                return Ok(id);
            else
                return InternalServerError();
        }
    }
}
