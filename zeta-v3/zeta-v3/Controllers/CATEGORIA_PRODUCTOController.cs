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
        private zeta_bdEntities10 db = new zeta_bdEntities10();

        [Route("api/categorias")]
        [HttpGet]
        public IHttpActionResult GetCategorias()
        {
           
            var categorias = from CATEGORIA_SUPERIOR in db.CATEGORIA_SUPERIOR
                             select new { CATEGORIA_SUPERIOR.ID_CATEGORIA_SUPERIOR, CATEGORIA_SUPERIOR.NOMBRE_CATEGORIA_SUPERIOR, CATEGORIA_SUPERIOR.DESCRIPCION_CATEGORIA_SUPERIOR };

            return Ok(categorias);
        }

        [Route("api/{id_categoria}/subcategorias")]
        [HttpGet]
        public IHttpActionResult Getsubcategorias(decimal id_categoria)
        {

            var subcategorias = from CATEGORIA_PRODUCTO in db.CATEGORIA_PRODUCTO
                                where CATEGORIA_PRODUCTO.ID_CATEGORIA == id_categoria
                                select new { CATEGORIA_PRODUCTO.ID_CATEGORIA, CATEGORIA_PRODUCTO.NOMBRE_CATEGORIA, CATEGORIA_PRODUCTO.DETALLE_CATEGORIA };

            return Ok(subcategorias);
        }

        [Route("api/categoria/{id_subcategoria}/caracteristicas")]
        [HttpGet]
        public IHttpActionResult Getcaracteristicas( decimal id_subcategoria)
        {

            var caracteristicas = from CARACTERISTICAS in db.CARACTERISTICAS
                                  where CARACTERISTICAS.ID_CATEGORIA == id_subcategoria
                                  select new { CARACTERISTICAS.ID_CARACTERISTICA, CARACTERISTICAS.NOMBRE_CARACTERISTICA };
            return Ok(caracteristicas);
        }

        [Route("api/categoria")]
        public IHttpActionResult PostCATEGORIA(CATEGORIA_SUPERIOR cATEGORIA_PRODUCTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            db.CATEGORIA_SUPERIOR.Add(cATEGORIA_PRODUCTO);
            db.SaveChanges();

            return Created("DefaultApi", new { cATEGORIA_PRODUCTO.ID_CATEGORIA_SUPERIOR , cATEGORIA_PRODUCTO.NOMBRE_CATEGORIA_SUPERIOR });

        }

        // POST: api/CATEGORIA_PRODUCTO
        [Route("api/categoria/subcategoria")]
        [ResponseType(typeof(CATEGORIA_PRODUCTO))]
        public IHttpActionResult PostCATEGORIA_PRODUCTO(CATEGORIA_PRODUCTO cATEGORIA_PRODUCTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            

            db.CATEGORIA_PRODUCTO.Add(cATEGORIA_PRODUCTO);
            db.SaveChanges();

            return Created("DefaultApi", new { cATEGORIA_PRODUCTO.ID_CATEGORIA, cATEGORIA_PRODUCTO.NOMBRE_CATEGORIA, cATEGORIA_PRODUCTO.ID_CATEGORIA_SUPERIOR });

        }

        // POST: CARACTERISTICAS
        [Route("api/categoria/subcategoria/caracteristicas")]
        public IHttpActionResult PostCARACTERISTICAS(CARACTERISTICAS cARACTERISTICAS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            db.CARACTERISTICAS.Add(cARACTERISTICAS);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cARACTERISTICAS.ID_CARACTERISTICA }, new { cARACTERISTICAS.ID_CARACTERISTICA, cARACTERISTICAS.NOMBRE_CARACTERISTICA });
        }


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