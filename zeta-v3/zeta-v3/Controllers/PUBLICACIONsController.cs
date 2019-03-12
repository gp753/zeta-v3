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
    public class PUBLICACIONsController : ApiController
    {

        private zeta_bdEntities10 db = new zeta_bdEntities10();

        [Route("api/publicacion/baner")]
        [HttpGet]
        public IHttpActionResult baner()
        {
            var baners = from PUBLICACION in db.PUBLICACION
                         orderby PUBLICACION.FECHA_PUBLICACION_2 descending
                         select new { PUBLICACION.ID_PUBLICACION, PUBLICACION.LINK_IMAGEN_PUBLICACION, PUBLICACION.LINK_PUBLICACION, PUBLICACION.DESCRIPCION_PUBLICACION };
            var resultado = baners.Take(3);

            return Ok(resultado);
        }

        // GET: api/PUBLICACIONs
        public IQueryable<PUBLICACION> GetPUBLICACION()
        {
            return db.PUBLICACION;
        }

        // GET: api/PUBLICACIONs/5
        [ResponseType(typeof(PUBLICACION))]
        public IHttpActionResult GetPUBLICACION(decimal id)
        {
            PUBLICACION pUBLICACION = db.PUBLICACION.Find(id);
            if (pUBLICACION == null)
            {
                return NotFound();
            }

            return Ok(pUBLICACION);
        }

        // PUT: api/PUBLICACIONs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPUBLICACION(decimal id, PUBLICACION pUBLICACION)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pUBLICACION.ID_PUBLICACION)
            {
                return BadRequest();
            }

            db.Entry(pUBLICACION).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PUBLICACIONExists(id))
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

        // POST: api/PUBLICACIONs
        [ResponseType(typeof(PUBLICACION))]
        public IHttpActionResult PostPUBLICACION(PUBLICACION pUBLICACION)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            pUBLICACION.FECHA_PUBLICACION_2 =DateTime.Today;
            pUBLICACION.ID_USUARIO = "1";
            db.PUBLICACION.Add(pUBLICACION);
            db.SaveChanges();
            
            return CreatedAtRoute("DefaultApi", new { id = pUBLICACION.ID_PUBLICACION }, new { pUBLICACION.ID_PUBLICACION, pUBLICACION.LINK_PUBLICACION, pUBLICACION.DESCRIPCION_PUBLICACION,pUBLICACION.LINK_IMAGEN_PUBLICACION });
        }

        // DELETE: api/PUBLICACIONs/5
        [ResponseType(typeof(PUBLICACION))]
        public IHttpActionResult DeletePUBLICACION(decimal id)
        {
            PUBLICACION pUBLICACION = db.PUBLICACION.Find(id);
            if (pUBLICACION == null)
            {
                return NotFound();
            }

            db.PUBLICACION.Remove(pUBLICACION);
            db.SaveChanges();

            return Ok(pUBLICACION);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PUBLICACIONExists(decimal id)
        {
            return db.PUBLICACION.Count(e => e.ID_PUBLICACION == id) > 0;
        }
    }
}