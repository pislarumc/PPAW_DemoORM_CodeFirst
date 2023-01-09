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
    public class EffectController : ApiController
    {
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<NivelAccesDate_ORM_CodeFirst.Models.Effect, API_02.Models.EffectModel>();
            cfg.CreateMap<API_02.Models.EffectModel, NivelAccesDate_ORM_CodeFirst.Models.Effect>();
        });

        private IEffects EffectsServiceLocal;
        public EffectController(IEffects EffectsServiceLocal)
        {
            this.EffectsServiceLocal = EffectsServiceLocal;
        }
        // GET: api/Effect
        public IEnumerable<API_02.Models.EffectModel> Get()
        {
            List<NivelAccesDate_ORM_CodeFirst.Models.Effect> effectsFromDB = EffectsServiceLocal.GetEffects();
            Mapper mapper = new Mapper(config);
            List<API_02.Models.EffectModel> effects = mapper
                .Map<List<NivelAccesDate_ORM_CodeFirst.Models.Effect>, List<API_02.Models.EffectModel>>(effectsFromDB);

            return effects;
        }

        // GET: api/Effect/5
        public IHttpActionResult Get(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            NivelAccesDate_ORM_CodeFirst.Models.Effect effectFromDB = EffectsServiceLocal.GetEffect(id);
            if (effectFromDB == null)
            {
                return NotFound();
            }
            Mapper mapper = new Mapper(config);
            API_02.Models.EffectModel effect = mapper
                .Map<NivelAccesDate_ORM_CodeFirst.Models.Effect, API_02.Models.EffectModel>(effectFromDB);
            return Ok(effect);
        }

        // POST: api/Effect
        public IHttpActionResult Post(API_02.Models.EffectModel effect)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Mapper mapper = new Mapper(config);
            NivelAccesDate_ORM_CodeFirst.Models.Effect effectForDB = mapper
                .Map<API_02.Models.EffectModel, NivelAccesDate_ORM_CodeFirst.Models.Effect>(effect);
            EffectsServiceLocal.AddEffect(effectForDB);

            return CreatedAtRoute("DefaultApi", new { id = effectForDB.EffectId }, effectForDB);
        }

        // PUT: api/Effect/5
        public IHttpActionResult Put(string id, API_02.Models.EffectModel effect)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (effect is null || id != effect.EffectId)
            {
                return BadRequest(ModelState);
            }

            Mapper mapper = new Mapper(config);
            NivelAccesDate_ORM_CodeFirst.Models.Effect effectForDB = mapper
                .Map<API_02.Models.EffectModel, NivelAccesDate_ORM_CodeFirst.Models.Effect>(effect);
            if (EffectsServiceLocal.UpdateEffect(id, effectForDB) == false)
            {
                return NotFound();
            }

            return Ok(effect);
        }

        // DELETE: api/Effect/5
        public IHttpActionResult Delete(string id)
        {
            if (EffectsServiceLocal.DeleteToggleEffect(id, true) == true)
                return Ok(id);
            else
                return BadRequest(id);
        }
    }
}
