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
    public class GASTOsController : ApiController
    {
        private zeta_bdEntities12 db = new zeta_bdEntities12();



        // GET: api/GASTOs/5
        [ResponseType(typeof(GASTO))]
        public IHttpActionResult GetGASTO(decimal id)
        {
            GASTO gASTO = db.GASTO.Find(id);
            if (gASTO == null)
            {
                return NotFound();
            }

            return Ok(gASTO);
        }

        [Route("api/gastos/all")]
        [HttpGet]
        public IHttpActionResult Ret_gastos()
        {
            /* var products = from PRODUCTO in db.PRODUCTO
                            join FOTO_PRODUCTO in db.FOTO_PRODUCTO on PRODUCTO.ID_PRODUCTO equals FOTO_PRODUCTO.ID_PRODUCTO
                            select new { PRODUCTO.ID_PRODUCTO, PRODUCTO.NOMBRE_PRODUCTO, PRODUCTO.PRECIO_VENTA, FOTO_PRODUCTO.LINK_FOTO };

             return Ok(products);*/
            var gastos = from GASTO in db.GASTO
                         orderby GASTO.FECHA_GASTO descending
                         select new { GASTO.ID_GASTO, GASTO.DETALLE_GASTO, GASTO.FECHA_GASTO, GASTO.MONTO_GASTO };

            return Ok(gastos);

        }

        // PUT: api/GASTOs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGASTO(decimal id, GASTO gASTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gASTO.ID_GASTO)
            {
                return BadRequest();
            }

            db.Entry(gASTO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GASTOExists(id))
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

        // POST: api/GASTOs
        [ResponseType(typeof(GASTO))]
        public IHttpActionResult PostGASTO(GASTO gASTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            gASTO.ID_USUARIO = "1";
            gASTO.FECHA_GASTO = DateTime.Today;
            db.GASTO.Add(gASTO);
            db.SaveChanges();
            //VER PARA AGREGAR FACTURA
            return CreatedAtRoute("DefaultApi", new { id = gASTO.ID_GASTO }, new { gASTO.ID_GASTO, gASTO.MONTO_GASTO, gASTO.DETALLE_GASTO});
        }

        // DELETE: api/GASTOs/5
        [ResponseType(typeof(GASTO))]
        public IHttpActionResult DeleteGASTO(decimal id)
        {
            GASTO gASTO = db.GASTO.Find(id);
            if (gASTO == null)
            {
                return NotFound();
            }

            db.GASTO.Remove(gASTO);
            db.SaveChanges();

            return Ok(gASTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GASTOExists(decimal id)
        {
            return db.GASTO.Count(e => e.ID_GASTO == id) > 0;
        }
    }
}