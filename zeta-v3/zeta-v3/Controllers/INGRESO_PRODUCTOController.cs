using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using zeta_v3.Models;

namespace zeta_v3.Controllers
{
    public class INGRESO_PRODUCTOController : ApiController
    {
        private zeta_bdEntities5 db = new zeta_bdEntities5();

        // GET: api/INGRESO_PRODUCTO
        public IQueryable<INGRESO_PRODUCTO> GetINGRESO_PRODUCTO()
        {
            return db.INGRESO_PRODUCTO;
        }

        // GET: api/INGRESO_PRODUCTO/5
        [ResponseType(typeof(INGRESO_PRODUCTO))]
        public IHttpActionResult GetINGRESO_PRODUCTO(decimal id)
        {
            INGRESO_PRODUCTO iNGRESO_PRODUCTO = db.INGRESO_PRODUCTO.Find(id);
            if (iNGRESO_PRODUCTO == null)
            {
                return NotFound();
            }

            return Ok(iNGRESO_PRODUCTO);
        }

        // PUT: api/INGRESO_PRODUCTO/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutINGRESO_PRODUCTO(decimal id, INGRESO_PRODUCTO iNGRESO_PRODUCTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iNGRESO_PRODUCTO.ID_INGRESO_PRODUCTO)
            {
                return BadRequest();
            }

            db.Entry(iNGRESO_PRODUCTO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!INGRESO_PRODUCTOExists(id))
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

        // POST: api/INGRESO_PRODUCTO
        [ResponseType(typeof(INGRESO_PRODUCTO))]
        public IHttpActionResult PostINGRESO_PRODUCTO(INGRESO_PRODUCTO iNGRESO_PRODUCTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            iNGRESO_PRODUCTO.ID_USUARIO = "1";
            db.INGRESO_PRODUCTO.Add(iNGRESO_PRODUCTO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = iNGRESO_PRODUCTO.ID_INGRESO_PRODUCTO }, new { iNGRESO_PRODUCTO.ID_PRODUCTO,iNGRESO_PRODUCTO.ID_COLOR, iNGRESO_PRODUCTO.ID_TAMANO});
        }



        // DELETE: api/INGRESO_PRODUCTO/5
        [ResponseType(typeof(INGRESO_PRODUCTO))]
        public IHttpActionResult DeleteINGRESO_PRODUCTO(decimal id)
        {
            INGRESO_PRODUCTO iNGRESO_PRODUCTO = db.INGRESO_PRODUCTO.Find(id);
            if (iNGRESO_PRODUCTO == null)
            {
                return NotFound();
            }

            db.INGRESO_PRODUCTO.Remove(iNGRESO_PRODUCTO);
            db.SaveChanges();

            return Ok(iNGRESO_PRODUCTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool INGRESO_PRODUCTOExists(decimal id)
        {
            return db.INGRESO_PRODUCTO.Count(e => e.ID_INGRESO_PRODUCTO == id) > 0;
        }
    }
}