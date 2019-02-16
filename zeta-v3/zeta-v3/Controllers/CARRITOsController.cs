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
using Microsoft.AspNet.Identity;

namespace zeta_v3.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CARRITOsController : ApiController
    {
        private zeta_bdEntities5 db = new zeta_bdEntities5();

        // GET: api/CARRITOs

        
        [Route("api/carritos/")]
        //[Authorize]
        [HttpGet]
        public async Task<IHttpActionResult> carrito_page()
        {
            /*  var id_usr = User.Identity.GetUserName();
              var usuario = from USUARIO in db.USUARIO
                               where USUARIO.EMAIL == id_usr
                               select USUARIO.ID_USUARIO;
              //hasta aca ya tengo el usuario
              */


            var id_carrito = from CARRITO in db.CARRITO
                             where CARRITO.ID_USUARIO == "1" //usuario.FirstOrDefault()
                             orderby CARRITO.FECHA_CREACION_CARRITO
                             select CARRITO.ID_CARRITO;
                             

           //consigo el id del carrito ORDENADO POR FECHA DE CREACION

            var carrito = from CANTIDAD_PRODUCTO in db.CANTIDAD_PRODUCTO
                          join PRODUCTO in db.PRODUCTO on CANTIDAD_PRODUCTO.ID_PRODUCTO equals PRODUCTO.ID_PRODUCTO
                          join FOTO_PRODUCTO in db.FOTO_PRODUCTO on PRODUCTO.ID_PRODUCTO equals FOTO_PRODUCTO.ID_PRODUCTO
                          where CANTIDAD_PRODUCTO.ID_CARRITO == id_carrito.FirstOrDefault()
                          select new { PRODUCTO.ID_PRODUCTO, PRODUCTO.NOMBRE_PRODUCTO, PRODUCTO.PRECIO_VENTA, FOTO_PRODUCTO.LINK_FOTO };

            //traigo los productos que pertencen a ese carrito

            var cantidad = carrito.ToList().Count();
            return Ok(new { carrito, cantidad });
        }

        // GET: api/CARRITOs/5
        [ResponseType(typeof(CARRITO))]
        public async Task<IHttpActionResult> GetCARRITO(decimal id)
        {
            CARRITO cARRITO = await db.CARRITO.FindAsync(id);
            if (cARRITO == null)
            {
                return NotFound();
            }

            return Ok(cARRITO);
        }

        // PUT: api/CARRITOs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCARRITO(decimal id, CARRITO cARRITO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cARRITO.ID_CARRITO)
            {
                return BadRequest();
            }

            db.Entry(cARRITO).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CARRITOExists(id))
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

        // POST: api/CARRITOs
        [ResponseType(typeof(CARRITO))]
        public async Task<IHttpActionResult> PostCARRITO(CARRITO cARRITO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CARRITO.Add(cARRITO);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cARRITO.ID_CARRITO }, cARRITO);
        }

        // DELETE: api/CARRITOs/5
        [ResponseType(typeof(CARRITO))]
        public async Task<IHttpActionResult> DeleteCARRITO(decimal id)
        {
            CARRITO cARRITO = await db.CARRITO.FindAsync(id);
            if (cARRITO == null)
            {
                return NotFound();
            }

            db.CARRITO.Remove(cARRITO);
            await db.SaveChangesAsync();

            return Ok(cARRITO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CARRITOExists(decimal id)
        {
            return db.CARRITO.Count(e => e.ID_CARRITO == id) > 0;
        }
    }
}