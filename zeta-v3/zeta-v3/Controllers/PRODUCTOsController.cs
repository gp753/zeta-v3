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
    public class PRODUCTOsController : ApiController
    {

        private zeta_bdEntities12 db = new zeta_bdEntities12();

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

            // join FOTOS_PRODUCTOS in db.FOTOS_PRODUCTOS on PRODUCTO.ID_PRODUCTO equals FOTOS_PRODUCTOS.ID_PRODUCTO
               //           join MULTIMEDIA in db.MULTIMEDIA on FOTOS_PRODUCTOS.ID_MULTIMEDIA equals MULTIMEDIA.ID_MULTIMEDIA

            var fotos = from FOTOS_PRODUCTOS in db.FOTOS_PRODUCTOS
                        join MULTIMEDIA in db.MULTIMEDIA on FOTOS_PRODUCTOS.ID_MULTIMEDIA equals MULTIMEDIA.ID_MULTIMEDIA
                        where FOTOS_PRODUCTOS.ID_PRODUCTO == pRODUCTO.ID_PRODUCTO
                        select MULTIMEDIA.LINK_MULTIMEDIA;
            var colores = from COLOR in db.COLOR
                          where COLOR.ID_PRODUCTO == pRODUCTO.ID_PRODUCTO
                          select new { COLOR.ID_COLOR, COLOR.NOMBRE_COLOR };
            var tamanos = from TAMANO in db.TAMANO
                          where TAMANO.ID_PRODUCTO == pRODUCTO.ID_PRODUCTO
                          select new { TAMANO.ID_TAMANO, TAMANO.NOMBRE_TAMANO };

            var categoria = from CATEGORIA_SUPERIOR in db.CATEGORIA_SUPERIOR
                            join CATEGORIA_PRODUCTO in db.CATEGORIA_PRODUCTO on CATEGORIA_SUPERIOR.ID_CATEGORIA_SUPERIOR equals CATEGORIA_PRODUCTO.ID_CATEGORIA_SUPERIOR
                            join PRODUCTOXCATEGORIA in db.PRODUCTOXCATEGORIA on CATEGORIA_PRODUCTO.ID_CATEGORIA equals PRODUCTOXCATEGORIA.ID_CATEGORIA
                            where PRODUCTOXCATEGORIA.ID_PRODUCTO == pRODUCTO.ID_PRODUCTO
                            select new { CATEGORIA_SUPERIOR.ID_CATEGORIA_SUPERIOR, CATEGORIA_SUPERIOR.NOMBRE_CATEGORIA_SUPERIOR, CATEGORIA_PRODUCTO.ID_CATEGORIA, CATEGORIA_PRODUCTO.NOMBRE_CATEGORIA };

                            
            return Ok(new { pRODUCTO.ID_PRODUCTO, pRODUCTO.NOMBRE_PRODUCTO,pRODUCTO.DESCRIPCION_CORTA, pRODUCTO.DESCRIPCION_LARGA, pRODUCTO.PRECIO_VENTA, pRODUCTO.PRECIO_OFERTA
                            , fotos, colores, tamanos, categoria});
        }

        [Route("api/producto/cantidad/{id_producto}/{id_color}/{id_tamano}")]
        [HttpGet]
        public async Task<IHttpActionResult> productos_stock_cantidad(decimal id_producto, decimal id_color, decimal id_tamano)
        {
            var cantidad = Productos_cantidad(id_producto, id_color, id_tamano);

            return Ok(cantidad);
        }

        public class aux_de_stock
        {
            public decimal ID_PRODUCTO { get; set; }
            public string NOMBRE_PRODUCTO { get; set; }
            public decimal? PRECIO_VENTA { get; set; }
            public string LINK_MULTIMEDIA { get; set; }
            public int? ESTADO_PUBLICACION { get; set; }
            public List<cantidades_aux> cantidades {get; set;}
        }

        [Route("api/productos/stock")]
        [HttpGet]
        public async Task<IHttpActionResult> productos_stock()
        {
            //falta devolver la cantidad en stock que hay de cada producto

            List<aux_de_stock> stock = new List<aux_de_stock>();
            var productos = from PRODUCTO in db.PRODUCTO
                            join FOTOS_PRODUCTOS in db.FOTOS_PRODUCTOS on PRODUCTO.ID_PRODUCTO equals FOTOS_PRODUCTOS.ID_PRODUCTO
                            join MULTIMEDIA in db.MULTIMEDIA on FOTOS_PRODUCTOS.ID_MULTIMEDIA equals MULTIMEDIA.ID_MULTIMEDIA
                            join INGRESO_PRODUCTO in db.INGRESO_PRODUCTO on PRODUCTO.ID_PRODUCTO equals INGRESO_PRODUCTO.ID_PRODUCTO
                            select new { PRODUCTO.ID_PRODUCTO,
                                        PRODUCTO.NOMBRE_PRODUCTO,
                                        PRODUCTO.PRECIO_VENTA,
                                        MULTIMEDIA.LINK_MULTIMEDIA,
                                        PRODUCTO.ESTADO_PUBLICACION                            
                                        };

            foreach (var producto2 in productos)
            {
                aux_de_stock stock1 = new aux_de_stock();
                var productos3 = (from INGRESO_PRODUCTO in db.INGRESO_PRODUCTO
                                 where INGRESO_PRODUCTO.ID_PRODUCTO == producto2.ID_PRODUCTO
                                 select new { INGRESO_PRODUCTO.ID_TAMANO, INGRESO_PRODUCTO.ID_COLOR }).Distinct();

                List<cantidades_aux> totales = new List<cantidades_aux>();

                foreach (var iNGRESO_ in productos3)
                {
                    cantidades_aux aux = new cantidades_aux();
                    aux.id_producto = producto2.ID_PRODUCTO;
                    aux.id_color = iNGRESO_.ID_COLOR;
                    aux.nombre_color = (from COLOR in db.COLOR
                                        where COLOR.ID_COLOR == iNGRESO_.ID_COLOR
                                        select COLOR.NOMBRE_COLOR).FirstOrDefault();
                    aux.id_tamano = iNGRESO_.ID_TAMANO;
                    aux.nombre_tamano = (from TAMANO in db.TAMANO
                                         where TAMANO.ID_TAMANO == iNGRESO_.ID_TAMANO
                                         select TAMANO.NOMBRE_TAMANO).FirstOrDefault();
                    aux.total = Productos_cantidad(producto2.ID_PRODUCTO, iNGRESO_.ID_COLOR, iNGRESO_.ID_TAMANO);

                    totales.Add(aux);
                }
                stock1.ID_PRODUCTO = producto2.ID_PRODUCTO;
                stock1.NOMBRE_PRODUCTO = producto2.NOMBRE_PRODUCTO;
                stock1.PRECIO_VENTA = producto2.PRECIO_VENTA;
                stock1.LINK_MULTIMEDIA = producto2.LINK_MULTIMEDIA;
                stock1.ESTADO_PUBLICACION = producto2.ESTADO_PUBLICACION;
                stock1.cantidades = totales;

                stock.Add(stock1);
                
            }


            return Ok(stock);
        }

        public class cantidades_aux
        {
            public decimal id_producto { get; set; }
            public decimal? id_color { get; set; }
            public string nombre_color { get; set; }
            public string nombre_tamano { get; set; }
            public decimal? id_tamano { get; set; }
            public decimal? total { get; set; }
        }

        

        [Route("api/producto/stock/{id_producto}")]
        [HttpGet]
        public async Task<IHttpActionResult> producto_stock(decimal id_producto)
        {
            //falta devolver la cantidad en stock que hay de cada producto



            var productos = (from INGRESO_PRODUCTO in db.INGRESO_PRODUCTO
                            where INGRESO_PRODUCTO.ID_PRODUCTO == id_producto
                            select new {INGRESO_PRODUCTO.ID_TAMANO, INGRESO_PRODUCTO.ID_COLOR }).Distinct();

            List<cantidades_aux> totales = new List<cantidades_aux>();

            foreach (var iNGRESO_ in productos)
            {
                cantidades_aux aux = new cantidades_aux();
                aux.id_producto = id_producto;
                aux.id_color = iNGRESO_.ID_COLOR;
                aux.nombre_color = (from COLOR in db.COLOR
                                    where COLOR.ID_COLOR == iNGRESO_.ID_COLOR
                                    select COLOR.NOMBRE_COLOR).FirstOrDefault();
                aux.id_tamano = iNGRESO_.ID_TAMANO;
                aux.nombre_tamano = (from TAMANO in db.TAMANO
                                     where TAMANO.ID_TAMANO == iNGRESO_.ID_TAMANO
                                     select TAMANO.NOMBRE_TAMANO).FirstOrDefault();
                aux.total = Productos_cantidad(id_producto, iNGRESO_.ID_COLOR, iNGRESO_.ID_TAMANO);

                totales.Add(aux);
            }
           




            
            return Ok(totales);
        }


        public decimal? Productos_cantidad(decimal id_producto, decimal? id_color, decimal? id_tamano)
        {

            decimal? id_color2 = id_color;
            if (id_color == null)
            {
                id_color2 = 0;
            }


            decimal? id_tamano2 = id_tamano;
            if (id_tamano == null)
            {
                id_tamano2 = 0;
            }
            

            


            decimal? ingreso = (from INGRESO_PRODUCTO in db.INGRESO_PRODUCTO
                           where INGRESO_PRODUCTO.ID_COLOR == id_color2 && INGRESO_PRODUCTO.ID_TAMANO == id_tamano2 && INGRESO_PRODUCTO.ID_PRODUCTO == id_producto
                           select INGRESO_PRODUCTO.CANTIDAD_INGRESO_PRODUCTO).Sum();
            if (ingreso == null )
            {
                ingreso = 0;
            }
            decimal? salida = (from CANTIDAD_PRODUCTO in db.CANTIDAD_PRODUCTO
                          where CANTIDAD_PRODUCTO.ID_COLOR == id_color2 && CANTIDAD_PRODUCTO.ID_TAMANO == id_tamano2 && CANTIDAD_PRODUCTO.ID_PRODUCTO == id_producto
                          select CANTIDAD_PRODUCTO.CANTIDAD_PRODUCTO_CARRITO).Sum();

            if (salida == null)
            {
                salida = 0;
            }

            decimal? cantidad = ingreso - salida;

            return cantidad;
        }

        [Route("api/productos/stock/vendedor")]
        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> productos_stock_vendedor()
        {
            //falta devolver la cantidad en stock que hay de cada producto

            string id_usuario = User.Identity.GetUserId();

            if (id_usuario == null)
            {
                return NotFound();
            }

            var productos = from PRODUCTO in db.PRODUCTO
                            join FOTOS_PRODUCTOS in db.FOTOS_PRODUCTOS on PRODUCTO.ID_PRODUCTO equals FOTOS_PRODUCTOS.ID_PRODUCTO
                            join MULTIMEDIA in db.MULTIMEDIA on FOTOS_PRODUCTOS.ID_MULTIMEDIA equals MULTIMEDIA.ID_MULTIMEDIA
                            join INGRESO_PRODUCTO in db.INGRESO_PRODUCTO on PRODUCTO.ID_PRODUCTO equals INGRESO_PRODUCTO.ID_PRODUCTO
                            where INGRESO_PRODUCTO.ID_USUARIO == id_usuario && PRODUCTO.ESTADO_PUBLICACION < 3
                            select new
                            {
                                PRODUCTO.ID_PRODUCTO,
                                PRODUCTO.NOMBRE_PRODUCTO,
                                PRODUCTO.PRECIO_VENTA,
                                MULTIMEDIA.LINK_MULTIMEDIA,
                                PRODUCTO.ESTADO_PUBLICACION,
                                cantidad = Productos_cantidad(PRODUCTO.ID_PRODUCTO, INGRESO_PRODUCTO.ID_TAMANO, INGRESO_PRODUCTO.ID_COLOR)
                            };

            //3 es eliminado
            return Ok(productos);
        }

        [Route("api/productos/busqueda")]
        [HttpPost]
        public async Task<IHttpActionResult> productos_prueba(AuxModel.buscar aux)
        {
            //falta devolver la cantidad en stock que hay de cada producto

            //palabra_clave

            /*var productos = from PRODUCTO in db.PRODUCTO
                            join CARACTERISTICA_PRODUCTO in db.CARACTERISTICA_PRODUCTO on PRODUCTO.ID_PRODUCTO equals CARACTERISTICA_PRODUCTO.ID_PRODUCTO
                            select new { PRODUCTO, CARACTERISTICA_PRODUCTO };*/

            List<PRODUCTO> productos = new List<PRODUCTO>();

            string palabra = " ";
            if (aux.PALABRA_CLAVE != null)
            {
                palabra = aux.PALABRA_CLAVE;
            }
            foreach (AuxModel.buscar.filtro filtro in aux.FILTROS)
            {
                   
                var busqueda = from PRODUCTO in db.PRODUCTO
                                join CARACTERISTICA_PRODUCTO in db.CARACTERISTICA_PRODUCTO on PRODUCTO.ID_PRODUCTO equals CARACTERISTICA_PRODUCTO.ID_PRODUCTO
                                where CARACTERISTICA_PRODUCTO.ID_CARACTERISTICA == filtro.ID_CARACTERISTICA && CARACTERISTICA_PRODUCTO.INFO_CAR == filtro.FILTRO && 
                                (PRODUCTO.NOMBRE_PRODUCTO.Contains(palabra) || PRODUCTO.DESCRIPCION_CORTA.Contains(palabra) || PRODUCTO.DESCRIPCION_LARGA.Contains(palabra))
                                select PRODUCTO;

                foreach (PRODUCTO producto in busqueda)
                {
                    productos.Add(producto);
                }
                
            }

            var resultado = from PRODUCTO in productos
                            join FOTOS_PRODUCTOS in db.FOTOS_PRODUCTOS on PRODUCTO.ID_PRODUCTO equals FOTOS_PRODUCTOS.ID_PRODUCTO
                            join MULTIMEDIA in db.MULTIMEDIA on FOTOS_PRODUCTOS.ID_MULTIMEDIA equals MULTIMEDIA.ID_MULTIMEDIA
                            join INGRESO_PRODUCTO in db.INGRESO_PRODUCTO on PRODUCTO.ID_PRODUCTO equals INGRESO_PRODUCTO.ID_PRODUCTO
                            select new { PRODUCTO.ID_PRODUCTO, PRODUCTO.NOMBRE_PRODUCTO, PRODUCTO.PRECIO_VENTA, MULTIMEDIA.LINK_MULTIMEDIA };

            
            
            return Ok(resultado);
        }
        
        [Route("api/productos/busqueda/{contenido}")]
        [HttpGet]
        public async Task<IHttpActionResult> productos_busqueda(string contenido)
        {
            //falta devolver la cantidad en stock que hay de cada producto

            var productos = from PRODUCTO in db.PRODUCTO
                            join FOTOS_PRODUCTOS in db.FOTOS_PRODUCTOS on PRODUCTO.ID_PRODUCTO equals FOTOS_PRODUCTOS.ID_PRODUCTO
                            join MULTIMEDIA in db.MULTIMEDIA on FOTOS_PRODUCTOS.ID_MULTIMEDIA equals MULTIMEDIA.ID_MULTIMEDIA
                            join INGRESO_PRODUCTO in db.INGRESO_PRODUCTO on PRODUCTO.ID_PRODUCTO equals INGRESO_PRODUCTO.ID_PRODUCTO
                            where PRODUCTO.NOMBRE_PRODUCTO.Contains(contenido) || PRODUCTO.DESCRIPCION_CORTA.Contains(contenido) || PRODUCTO.DESCRIPCION_LARGA.Contains(contenido)
                            select new { PRODUCTO.ID_PRODUCTO, PRODUCTO.NOMBRE_PRODUCTO, PRODUCTO.PRECIO_VENTA, MULTIMEDIA.LINK_MULTIMEDIA };

            return Ok(productos);

        }

        [Route("api/productos/sub_categoria/{id_sub}")]
        [HttpGet]
        public async Task<IHttpActionResult> productos_sub_categoria(decimal id_sub)
        {
            var productos = from PRODUCTO in db.PRODUCTO
                            join FOTOS_PRODUCTOS in db.FOTOS_PRODUCTOS on PRODUCTO.ID_PRODUCTO equals FOTOS_PRODUCTOS.ID_PRODUCTO
                            join MULTIMEDIA in db.MULTIMEDIA on FOTOS_PRODUCTOS.ID_MULTIMEDIA equals MULTIMEDIA.ID_MULTIMEDIA
                            join INGRESO_PRODUCTO in db.INGRESO_PRODUCTO on PRODUCTO.ID_PRODUCTO equals INGRESO_PRODUCTO.ID_PRODUCTO
                           join PRODUCTOXCATEGORIA in db.PRODUCTOXCATEGORIA on PRODUCTO.ID_PRODUCTO equals PRODUCTOXCATEGORIA.ID_PRODUCTO
                           join CATEGORIA_PRODUCTO in db.CATEGORIA_PRODUCTO on PRODUCTOXCATEGORIA.ID_CATEGORIA equals CATEGORIA_PRODUCTO.ID_CATEGORIA
                            select new { PRODUCTO.ID_PRODUCTO, PRODUCTO.NOMBRE_PRODUCTO, PRODUCTO.PRECIO_VENTA, MULTIMEDIA.LINK_MULTIMEDIA };

            return Ok(productos);
        }



        [Route ("api/productos/nuevos") ]
        [HttpGet]
        public async Task<IHttpActionResult> productos_nuevos()
        {
            var products = from PRODUCTO in db.PRODUCTO
                           join FOTOS_PRODUCTOS in db.FOTOS_PRODUCTOS on PRODUCTO.ID_PRODUCTO equals FOTOS_PRODUCTOS.ID_PRODUCTO
                           join MULTIMEDIA in db.MULTIMEDIA on FOTOS_PRODUCTOS.ID_MULTIMEDIA equals MULTIMEDIA.ID_MULTIMEDIA
                           select new { PRODUCTO.ID_PRODUCTO, PRODUCTO.NOMBRE_PRODUCTO, PRODUCTO.PRECIO_VENTA,  MULTIMEDIA.LINK_MULTIMEDIA };

            return Ok(products.Take(3));
        }

        [Route("api/productos/publicitados")]
        [HttpGet]
        public async Task<IHttpActionResult> productos_publicitados()
        {
            var products = from PRODUCTO in db.PRODUCTO
                           join FOTOS_PRODUCTOS in db.FOTOS_PRODUCTOS on PRODUCTO.ID_PRODUCTO equals FOTOS_PRODUCTOS.ID_PRODUCTO
                           join MULTIMEDIA in db.MULTIMEDIA on FOTOS_PRODUCTOS.ID_MULTIMEDIA equals MULTIMEDIA.ID_MULTIMEDIA
                           select new { PRODUCTO.ID_PRODUCTO, PRODUCTO.NOMBRE_PRODUCTO, PRODUCTO.PRECIO_VENTA, MULTIMEDIA.LINK_MULTIMEDIA };

            return Ok(products.Take(3));
        }

        [Route("api/productos/pendientes")]
        [HttpGet]
        public async Task<IHttpActionResult> productos_pendientes()
        {
            var products = from PRODUCTO in db.PRODUCTO
                           join FOTOS_PRODUCTOS in db.FOTOS_PRODUCTOS on PRODUCTO.ID_PRODUCTO equals FOTOS_PRODUCTOS.ID_PRODUCTO
                           join MULTIMEDIA in db.MULTIMEDIA on FOTOS_PRODUCTOS.ID_MULTIMEDIA equals MULTIMEDIA.ID_MULTIMEDIA
                           where PRODUCTO.ESTADO_PUBLICACION == 0
                           select new { PRODUCTO.ID_PRODUCTO, PRODUCTO.NOMBRE_PRODUCTO, PRODUCTO.PRECIO_VENTA, MULTIMEDIA.LINK_MULTIMEDIA };

            return Ok(products);
        }

        [Route("api/productos/populares")]
        [HttpGet]
        public async Task<IHttpActionResult> productos_populares()
        {
            var products = from PRODUCTO in db.PRODUCTO
                           join FOTOS_PRODUCTOS in db.FOTOS_PRODUCTOS on PRODUCTO.ID_PRODUCTO equals FOTOS_PRODUCTOS.ID_PRODUCTO
                           join MULTIMEDIA in db.MULTIMEDIA on FOTOS_PRODUCTOS.ID_MULTIMEDIA equals MULTIMEDIA.ID_MULTIMEDIA
                           select new { PRODUCTO.ID_PRODUCTO, PRODUCTO.NOMBRE_PRODUCTO, PRODUCTO.PRECIO_VENTA, MULTIMEDIA.LINK_MULTIMEDIA };

            return Ok(products.Take(3));
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
        [Authorize]
        public async Task<IHttpActionResult> PostPRODUCTO(PRODUCTO pRODUCTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string id_usuario = User.Identity.GetUserId();
            pRODUCTO.ESTADO_PUBLICACION = 0;
            pRODUCTO.FECHA_PUBLICACION = DateTime.Today;
            db.PRODUCTO.Add(pRODUCTO);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pRODUCTO.ID_PRODUCTO }, new { pRODUCTO.ID_PRODUCTO });
        }

        // POST: api/PRODUCTOs 
        [Route("api/productos/cambiar_estado")]
        public async Task<IHttpActionResult> cargar_categoria(AuxModel.producto_aceptado aux)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PRODUCTO pRODUCTO = await db.PRODUCTO.FindAsync(aux.ID_PRODUCTO);

            if(pRODUCTO == null)
            {
                return NotFound();
            }

            pRODUCTO.ESTADO_PUBLICACION = aux.ESTADO;


            db.Entry(pRODUCTO).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PRODUCTOExists(aux.ID_PRODUCTO))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            

            return Ok();
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