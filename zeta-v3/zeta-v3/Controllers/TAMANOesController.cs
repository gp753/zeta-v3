using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using zeta_v3.Models;

namespace zeta_v3.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TAMANOesController : ApiController
    {
        private zeta_bdEntities7 db = new zeta_bdEntities7();

        // GET: api/TAMANOes
        public IQueryable<TAMANO> GetTAMANO()
        {
            return db.TAMANO;
        }

        // GET: api/TAMANOes/5
        [ResponseType(typeof(TAMANO))]
        public IHttpActionResult GetTAMANO(decimal id)
        {
            TAMANO tAMANO = db.TAMANO.Find(id);
            if (tAMANO == null)
            {
                return NotFound();
            }

            return Ok(tAMANO);
        }

        // PUT: api/TAMANOes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTAMANO(decimal id, TAMANO tAMANO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tAMANO.ID_TAMANO)
            {
                return BadRequest();
            }

            db.Entry(tAMANO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TAMANOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TAMANOes
        [ResponseType(typeof(TAMANO))]
        public IHttpActionResult PostTAMANO(TAMANO tAMANO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TAMANO.Add(tAMANO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tAMANO.ID_TAMANO }, tAMANO);
        }

        // DELETE: api/TAMANOes/5
        [ResponseType(typeof(TAMANO))]
        public IHttpActionResult DeleteTAMANO(decimal id)
        {
            TAMANO tAMANO = db.TAMANO.Find(id);
            if (tAMANO == null)
            {
                return NotFound();
            }

            db.TAMANO.Remove(tAMANO);
            db.SaveChanges();

            return Ok(tAMANO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TAMANOExists(decimal id)
        {
            return db.TAMANO.Count(e => e.ID_TAMANO == id) > 0;
        }
    }
}