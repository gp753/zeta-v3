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
    public class CATEGORIA_PRODUCTOController : ApiController
    {
        private zeta_bdEntities7 db = new zeta_bdEntities7();

        [Route("api/categoria_producto")]
        [HttpGet]
        public IHttpActionResult GetCategorias()
        {
            /*  var products = from PRODUCTO in db.PRODUCTO
                             join FOTO_PRODUCTO in db.FOTO_PRODUCTO on PRODUCTO.ID_PRODUCTO equals FOTO_PRODUCTO.ID_PRODUCTO
                             select new { PRODUCTO.ID_PRODUCTO, PRODUCTO.NOMBRE_PRODUCTO, PRODUCTO.PRECIO_VENTA, FOTO_PRODUCTO.LINK_FOTO };*/
            var categorias = from CATEGORIA_PRODUCTO in db.CATEGORIA_PRODUCTO
                             select new { CATEGORIA_PRODUCTO.ID_CATEGORIA, CATEGORIA_PRODUCTO.NOMBRE_CATEGORIA };

            return Ok(categorias);
        }

        // POST: api/CATEGORIA_PRODUCTO
        [Route("api/categoria_producto")]
        [ResponseType(typeof(CATEGORIA_PRODUCTO))]
        public IHttpActionResult PostCATEGORIA_PRODUCTO(CATEGORIA_PRODUCTO cATEGORIA_PRODUCTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CATEGORIA_PRODUCTO.Add(cATEGORIA_PRODUCTO);
            db.SaveChanges();

            return Created("DefaultApi", new { cATEGORIA_PRODUCTO.ID_CATEGORIA, cATEGORIA_PRODUCTO.NOMBRE_CATEGORIA });

        }

        /* // PUT: api/CATEGORIA_PRODUCTO/5
         [ResponseType(typeof(void))]
         public IHttpActionResult PutCATEGORIA_PRODUCTO(decimal id, CATEGORIA_PRODUCTO cATEGORIA_PRODUCTO)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }

             if (id != cATEGORIA_PRODUCTO.ID_CATEGORIA)
             {
                 return BadRequest();
             }

             db.Entry(cATEGORIA_PRODUCTO).State = EntityState.Modified;

             try
             {
                 db.SaveChanges();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!CATEGORIA_PRODUCTOExists(id))
                 {
                     return NotFound();
                 }
                 else
                 {
                     throw;
                 }
             }

             return StatusCode(HttpStatusCode.NoContent);
         }*/


        // DELETE: api/CATEGORIA_PRODUCTO/5
        [ResponseType(typeof(CATEGORIA_PRODUCTO))]
        public IHttpActionResult DeleteCATEGORIA_PRODUCTO(decimal id)
        {
            CATEGORIA_PRODUCTO cATEGORIA_PRODUCTO = db.CATEGORIA_PRODUCTO.Find(id);
            if (cATEGORIA_PRODUCTO == null)
            {
                return NotFound();
            }

            db.CATEGORIA_PRODUCTO.Remove(cATEGORIA_PRODUCTO);
            db.SaveChanges();

            return Ok(cATEGORIA_PRODUCTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CATEGORIA_PRODUCTOExists(decimal id)
        {
            return db.CATEGORIA_PRODUCTO.Count(e => e.ID_CATEGORIA == id) > 0;
        }
    }
}