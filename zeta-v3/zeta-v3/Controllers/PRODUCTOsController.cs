using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using zeta_v3.Models;
using System.Web.Http.Cors;

namespace zeta_v3.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PRODUCTOsController : ApiController
    {
        
        private zeta_bdEntities3 db = new zeta_bdEntities3();

        // GET: api/PRODUCTOs
        public IQueryable<PRODUCTO> GetPRODUCTO()
        {
            return db.PRODUCTO;
        }

        // GET: api/PRODUCTOs/5
        [ResponseType(typeof(PRODUCTO))]
        public async Task<IHttpActionResult> GetPRODUCTO(decimal id)
        {
            PRODUCTO pRODUCTO = await db.PRODUCTO.FindAsync(id);
            if (pRODUCTO == null)
            {
                return NotFound();
            }

            return Ok(pRODUCTO);
        }

        [Route("api/publicacion/baner")]
        [HttpGet]
        public async Task<IHttpActionResult> baner()
        {
            
            var link = "https://about.canva.com/wp-content/uploads/sites/3/2017/02/congratulations_-banner.png";
            return Ok(new { link });
        }
        [Route("api/carritos/page")]
        [HttpGet]
        public async Task<IHttpActionResult> carrito_page()
        {
            
            var banr = 350000;
            return Ok(banr);
        }
        [Route ("api/productos/nuevos") ]
        [HttpGet]
        public async Task<IHttpActionResult> productos_nuevos()
        {
            var products = from PRODUCTO in db.PRODUCTO
                           join FOTO_PRODUCTO in db.FOTO_PRODUCTO on PRODUCTO.ID_PRODUCTO equals FOTO_PRODUCTO.ID_PRODUCTO
                           select new { PRODUCTO.ID_PRODUCTO, PRODUCTO.NOMBRE_PRODUCTO, PRODUCTO.PRECIO_VENTA,  FOTO_PRODUCTO.LINK_FOTO };

            return Ok(products);
        }

        [Route("api/productos/publicitados")]
        [HttpGet]
        public async Task<IHttpActionResult> productos_publicitados()
        {
            var products = from PRODUCTO in db.PRODUCTO
                           join FOTO_PRODUCTO in db.FOTO_PRODUCTO on PRODUCTO.ID_PRODUCTO equals FOTO_PRODUCTO.ID_PRODUCTO
                           select new { PRODUCTO.ID_PRODUCTO, PRODUCTO.NOMBRE_PRODUCTO, PRODUCTO.PRECIO_VENTA, FOTO_PRODUCTO.LINK_FOTO };

            return Ok(products);
        }

        [Route("api/productos/populares")]
        [HttpGet]
        public async Task<IHttpActionResult> productos_populares()
        {
            var products = from PRODUCTO in db.PRODUCTO
                           join FOTO_PRODUCTO in db.FOTO_PRODUCTO on PRODUCTO.ID_PRODUCTO equals FOTO_PRODUCTO.ID_PRODUCTO
                           select new { PRODUCTO.ID_PRODUCTO, PRODUCTO.NOMBRE_PRODUCTO, PRODUCTO.PRECIO_VENTA, FOTO_PRODUCTO.LINK_FOTO };

            return Ok(products);
        }

        // PUT: api/PRODUCTOs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPRODUCTO(decimal id, PRODUCTO pRODUCTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pRODUCTO.ID_PRODUCTO)
            {
                return BadRequest();
            }

            db.Entry(pRODUCTO).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PRODUCTOExists(id))
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

        // POST: api/PRODUCTOs
        [ResponseType(typeof(PRODUCTO))]
        public async Task<IHttpActionResult> PostPRODUCTO(PRODUCTO pRODUCTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PRODUCTO.Add(pRODUCTO);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pRODUCTO.ID_PRODUCTO }, pRODUCTO);
        }

        // DELETE: api/PRODUCTOs/5
        [ResponseType(typeof(PRODUCTO))]
        public async Task<IHttpActionResult> DeletePRODUCTO(decimal id)
        {
            PRODUCTO pRODUCTO = await db.PRODUCTO.FindAsync(id);
            if (pRODUCTO == null)
            {
                return NotFound();
            }

            db.PRODUCTO.Remove(pRODUCTO);
            await db.SaveChangesAsync();

            return Ok(pRODUCTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PRODUCTOExists(decimal id)
        {
            return db.PRODUCTO.Count(e => e.ID_PRODUCTO == id) > 0;
        }
    }
}