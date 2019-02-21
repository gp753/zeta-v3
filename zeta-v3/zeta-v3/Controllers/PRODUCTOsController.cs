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

        private zeta_bdEntities7 db = new zeta_bdEntities7();

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

            var fotos = from FOTO_PRODUCTO in db.FOTO_PRODUCTO
                        where FOTO_PRODUCTO.ID_PRODUCTO == pRODUCTO.ID_PRODUCTO
                        select FOTO_PRODUCTO.LINK_FOTO;
            var colores = from COLOR in db.COLOR
                          where COLOR.ID_PRODUCTO == pRODUCTO.ID_PRODUCTO
                          select new { COLOR.ID_COLOR, COLOR.NOMBRE_COLOR };
            var tamanos = from TAMANO in db.TAMANO
                          where TAMANO.ID_PRODUCTO == pRODUCTO.ID_PRODUCTO
                          select new { TAMANO.ID_TAMANO, TAMANO.NOMBRE_TAMANO };
            return Ok(new { pRODUCTO.ID_PRODUCTO, pRODUCTO.NOMBRE_PRODUCTO,pRODUCTO.DESCRIPCION_CORTA, pRODUCTO.DESCRIPCION_LARGA, pRODUCTO.PRECIO_VENTA, pRODUCTO.PRECIO_OFERTA
                            , fotos, colores, tamanos});
        }

        [Route("api/publicacion/baner")]
        [HttpGet]
        public async Task<IHttpActionResult> baner()
        {
            
            var link = "https://about.canva.com/wp-content/uploads/sites/3/2017/02/congratulations_-banner.png";
            return Ok(new { link });
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

        [Route("api/productos/{id}/colores_tamanos")]
        [HttpGet]
        public async Task<IHttpActionResult> get_productos_colores_tamanos(decimal id)
        {
            var color = from COLOR in db.COLOR
                          where COLOR.ID_PRODUCTO == id
                          select new { COLOR.ID_COLOR, COLOR.NOMBRE_COLOR };
            var tamano = from TAMANO in db.TAMANO
                          where TAMANO.ID_PRODUCTO == id
                          select new { TAMANO.ID_TAMANO, TAMANO.NOMBRE_TAMANO };

            return Ok(new { colores = color, tamanos = tamano });
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

            pRODUCTO.ESTADO_PUBLICACION = 0;
            pRODUCTO.FECHA_PUBLICACION = DateTime.Today;
            db.PRODUCTO.Add(pRODUCTO);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pRODUCTO.ID_PRODUCTO }, new { pRODUCTO.ID_PRODUCTO });
        }

        // POST: api/PRODUCTOs categorias
        [Route("api/productos/categoria")]
        public async Task<IHttpActionResult> cargar_categoria(AuxModel.catdeproductos aux)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PRODUCTOXCATEGORIA pcat = new PRODUCTOXCATEGORIA();
            pcat.ID_CATEGORIA = aux.ID_CATEGORIA;
            pcat.ID_PRODUCTO = aux.ID_PRODUCTO;
            
            
            db.PRODUCTOXCATEGORIA.Add(pcat);
            await db.SaveChangesAsync();

            return Created("DefaultApi",  new { pcat.ID_CATEGORIA, pcat.ID_PRODUCTO });
        }

        // POST: api/PRODUCTOs/caracteristica
        [Route("api/productos/caracteristica")]
        public async Task<IHttpActionResult> cargar_caracteristica(AuxModel.caracteristicaproduct aux)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            CARACTERISTICA_PRODUCTO pcar = new CARACTERISTICA_PRODUCTO();
            pcar.ID_PRODUCTO = aux.ID_PRODUCTO;
            pcar.ID_CARACTERISTICA = aux.ID_CARACTERISTICA;
            pcar.INFO_CAR = aux.informacion;
            
            db.CARACTERISTICA_PRODUCTO.Add(pcar);
            await db.SaveChangesAsync();

            return Created("DefaultApi", new { pcar.ID_CARACTERISTICA, pcar.ID_PRODUCTO });
        }

        // POST: api/productos/tamano_color
        [Route("api/productos/tamano_color")]
        public async Task<IHttpActionResult> cargar_tam_color(AuxModel.tamanosycolores aux)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TAMANO tMANO= new TAMANO();
            COLOR cOLOR = new COLOR();

            tMANO.ID_PRODUCTO = aux.ID_PRODUCTO;
            tMANO.NOMBRE_TAMANO = aux.NOMBRE_TAMANO;

            cOLOR.ID_PRODUCTO = aux.ID_PRODUCTO;
            cOLOR.NOMBRE_COLOR = aux.NOMBRE_COLOR;


            db.COLOR.Add(cOLOR);
            db.TAMANO.Add(tMANO);
           
            await db.SaveChangesAsync();

            return Created("DefaultApi",  new { aux.ID_PRODUCTO, cOLOR.ID_COLOR, cOLOR.NOMBRE_COLOR, tMANO.ID_TAMANO, tMANO.NOMBRE_TAMANO });
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