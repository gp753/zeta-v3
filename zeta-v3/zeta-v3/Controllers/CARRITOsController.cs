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
        private zeta_bdEntities12 db = new zeta_bdEntities12();

        //CARGAR CARRITO
        [Route("api/carrito/")]
        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> Cargar_carrito(AuxModel.productoacarrito aux)
        {
            string id_usuario = User.Identity.GetUserId();
            var id_carrito = from CARRITO in db.CARRITO
                             where CARRITO.ID_USUARIO == id_usuario && CARRITO.TIPO_CARRITO == 1
                             select CARRITO.ID_CARRITO;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            if(aux.ID_COLOR == 0 && aux.ID_TAMANO == 0 )
            {

                var producto_extiste = from CANTIDAD_PRODUCTO in db.CANTIDAD_PRODUCTO
                                       where CANTIDAD_PRODUCTO.ID_PRODUCTO == aux.ID_PRODUCTO 
                                       select CANTIDAD_PRODUCTO.ID_CANTIDAD_PRODUCTO;
                
                if (producto_extiste.ToList().Count() == 0)
                {
                    CANTIDAD_PRODUCTO add_carrito = new CANTIDAD_PRODUCTO();
                                                         
                    add_carrito.ID_CARRITO = id_carrito.ToList().First();
                    //  add_carrito.ID_COLOR = aux.ID_COLOR;
                    add_carrito.ID_PRODUCTO = aux.ID_PRODUCTO;
                    // add_carrito.ID_TAMANO = aux.ID_TAMANO;
                    add_carrito.CANTIDAD_PRODUCTO_CARRITO = aux.CANTIDAD;

                    db.CANTIDAD_PRODUCTO.Add(add_carrito);
                    await db.SaveChangesAsync();
                }
                else
                {
                    CANTIDAD_PRODUCTO cANTIDAD_PRODUCTO = await db.CANTIDAD_PRODUCTO.FindAsync(producto_extiste.ToList().FirstOrDefault());
                    var productos = cANTIDAD_PRODUCTO.CANTIDAD_PRODUCTO_CARRITO + aux.CANTIDAD;
                    cANTIDAD_PRODUCTO.CANTIDAD_PRODUCTO_CARRITO = productos;

                    db.Entry(cANTIDAD_PRODUCTO).State = EntityState.Modified;

                    try
                    {
                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }
                }
            }
            else
            {
                if(aux.ID_COLOR > 0 && aux.ID_TAMANO == 0)
                {
                    var producto_extiste = from CANTIDAD_PRODUCTO in db.CANTIDAD_PRODUCTO
                                           where CANTIDAD_PRODUCTO.ID_PRODUCTO == aux.ID_PRODUCTO && CANTIDAD_PRODUCTO.ID_COLOR == aux.ID_COLOR
                                           select CANTIDAD_PRODUCTO.ID_CANTIDAD_PRODUCTO;

                    if (producto_extiste.ToList().Count() == 0)
                    {
                        CANTIDAD_PRODUCTO add_carrito = new CANTIDAD_PRODUCTO();




                        add_carrito.ID_CARRITO = id_carrito.ToList().First(); 
                        add_carrito.ID_COLOR = aux.ID_COLOR;
                        add_carrito.ID_PRODUCTO = aux.ID_PRODUCTO;
                        // add_carrito.ID_TAMANO = aux.ID_TAMANO;
                        add_carrito.CANTIDAD_PRODUCTO_CARRITO = aux.CANTIDAD;

                        db.CANTIDAD_PRODUCTO.Add(add_carrito);
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        CANTIDAD_PRODUCTO cANTIDAD_PRODUCTO = await db.CANTIDAD_PRODUCTO.FindAsync(producto_extiste.ToList().FirstOrDefault());
                        var productos = cANTIDAD_PRODUCTO.CANTIDAD_PRODUCTO_CARRITO + aux.CANTIDAD;
                        cANTIDAD_PRODUCTO.CANTIDAD_PRODUCTO_CARRITO = productos;

                        db.Entry(cANTIDAD_PRODUCTO).State = EntityState.Modified;

                        try
                        {
                            await db.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            throw;
                        }
                    }
                }
                else
                {
                    if (aux.ID_COLOR == 0 && aux.ID_TAMANO > 0)
                    {
                        var producto_extiste = from CANTIDAD_PRODUCTO in db.CANTIDAD_PRODUCTO
                                               where CANTIDAD_PRODUCTO.ID_PRODUCTO == aux.ID_PRODUCTO  && CANTIDAD_PRODUCTO.ID_TAMANO == aux.ID_TAMANO
                                               select CANTIDAD_PRODUCTO.ID_CANTIDAD_PRODUCTO;

                        if (producto_extiste.ToList().Count() == 0)
                        {
                            CANTIDAD_PRODUCTO add_carrito = new CANTIDAD_PRODUCTO();




                            add_carrito.ID_CARRITO = id_carrito.ToList().First();
                            //  add_carrito.ID_COLOR = aux.ID_COLOR;
                            add_carrito.ID_PRODUCTO = aux.ID_PRODUCTO;
                            add_carrito.ID_TAMANO = aux.ID_TAMANO;
                            add_carrito.CANTIDAD_PRODUCTO_CARRITO = aux.CANTIDAD;

                            db.CANTIDAD_PRODUCTO.Add(add_carrito);
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            CANTIDAD_PRODUCTO cANTIDAD_PRODUCTO = await db.CANTIDAD_PRODUCTO.FindAsync(producto_extiste.ToList().FirstOrDefault());
                            var productos = cANTIDAD_PRODUCTO.CANTIDAD_PRODUCTO_CARRITO + aux.CANTIDAD;
                            cANTIDAD_PRODUCTO.CANTIDAD_PRODUCTO_CARRITO = productos;

                            db.Entry(cANTIDAD_PRODUCTO).State = EntityState.Modified;

                            try
                            {
                                await db.SaveChangesAsync();
                            }
                            catch (DbUpdateConcurrencyException)
                            {
                                throw;
                            }
                        }
                    }
                    else
                    {
                        var producto_extiste = from CANTIDAD_PRODUCTO in db.CANTIDAD_PRODUCTO
                                               where CANTIDAD_PRODUCTO.ID_PRODUCTO == aux.ID_PRODUCTO && CANTIDAD_PRODUCTO.ID_COLOR == aux.ID_COLOR && CANTIDAD_PRODUCTO.ID_TAMANO == aux.ID_TAMANO
                                               select CANTIDAD_PRODUCTO.ID_CANTIDAD_PRODUCTO;

                        if (producto_extiste.ToList().Count() == 0)
                        {
                            CANTIDAD_PRODUCTO add_carrito = new CANTIDAD_PRODUCTO();




                            add_carrito.ID_CARRITO = id_carrito.ToList().First();
                            add_carrito.ID_COLOR = aux.ID_COLOR;
                            add_carrito.ID_PRODUCTO = aux.ID_PRODUCTO;
                            add_carrito.ID_TAMANO = aux.ID_TAMANO;
                            add_carrito.CANTIDAD_PRODUCTO_CARRITO = aux.CANTIDAD;

                            db.CANTIDAD_PRODUCTO.Add(add_carrito);
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            CANTIDAD_PRODUCTO cANTIDAD_PRODUCTO = await db.CANTIDAD_PRODUCTO.FindAsync(producto_extiste.ToList().FirstOrDefault());
                            var productos = cANTIDAD_PRODUCTO.CANTIDAD_PRODUCTO_CARRITO + aux.CANTIDAD;
                            cANTIDAD_PRODUCTO.CANTIDAD_PRODUCTO_CARRITO = productos;

                            db.Entry(cANTIDAD_PRODUCTO).State = EntityState.Modified;

                            try
                            {
                                await db.SaveChangesAsync();
                            }
                            catch (DbUpdateConcurrencyException)
                            {
                                throw;
                            }
                        }
                    }
                }
            }
            
            var carrito = from CANTIDAD_PRODUCTO in db.CANTIDAD_PRODUCTO
                          join PRODUCTO in db.PRODUCTO on CANTIDAD_PRODUCTO.ID_PRODUCTO equals PRODUCTO.ID_PRODUCTO
                          join FOTOS_PRODUCTOS in db.FOTOS_PRODUCTOS on PRODUCTO.ID_PRODUCTO equals FOTOS_PRODUCTOS.ID_PRODUCTO
                          join MULTIMEDIA in db.MULTIMEDIA on FOTOS_PRODUCTOS.ID_MULTIMEDIA equals MULTIMEDIA.ID_MULTIMEDIA
                          where CANTIDAD_PRODUCTO.ID_CARRITO == id_carrito.ToList().First() //cambiar por el id de carrito del usuario
            select new { PRODUCTO.ID_PRODUCTO, PRODUCTO.NOMBRE_PRODUCTO, PRODUCTO.PRECIO_VENTA,CANTIDAD_PRODUCTO.CANTIDAD_PRODUCTO_CARRITO, MULTIMEDIA.LINK_MULTIMEDIA };


            var cantidad = carrito.ToList().Count();
            return Created("DefaultApi", new { carrito, cantidad });
           
            

        }

        // DELETE: api/CARRITOs/5
        [Route("api/carrito/eliminar")]
        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> EliminarCarritoProducto(AuxModel.productoacarrito aux)
        {
            //pulir esta parte para contemplar los null
            string id_usuario = User.Identity.GetUserId();
            var id_carrito = from CARRITO in db.CARRITO
                             where CARRITO.ID_USUARIO == id_usuario && CARRITO.TIPO_CARRITO == 1
                             select CARRITO.ID_CARRITO;

            var seleccion = new decimal();
            if(aux.ID_COLOR > 0 && aux.ID_TAMANO > 0)
            {
                 seleccion = (from CANTIDAD_PRODUCTO in db.CANTIDAD_PRODUCTO
                                where CANTIDAD_PRODUCTO.ID_CARRITO == id_carrito.ToList().First() && CANTIDAD_PRODUCTO.ID_COLOR == aux.ID_COLOR && CANTIDAD_PRODUCTO.ID_PRODUCTO == aux.ID_PRODUCTO && CANTIDAD_PRODUCTO.ID_TAMANO == aux.ID_TAMANO
                                select CANTIDAD_PRODUCTO.ID_CANTIDAD_PRODUCTO).ToList().FirstOrDefault();
            }
            else
            {
                if(aux.ID_COLOR == 0 && aux.ID_TAMANO > 0)
                {
                    seleccion = (from CANTIDAD_PRODUCTO in db.CANTIDAD_PRODUCTO
                                 where CANTIDAD_PRODUCTO.ID_CARRITO == id_carrito.ToList().First() && CANTIDAD_PRODUCTO.ID_PRODUCTO == aux.ID_PRODUCTO && CANTIDAD_PRODUCTO.ID_TAMANO == aux.ID_TAMANO
                                 select CANTIDAD_PRODUCTO.ID_CANTIDAD_PRODUCTO).ToList().FirstOrDefault();
                }
                else
                {
                    seleccion = (from CANTIDAD_PRODUCTO in db.CANTIDAD_PRODUCTO
                                 where CANTIDAD_PRODUCTO.ID_CARRITO == id_carrito.ToList().First() &&  CANTIDAD_PRODUCTO.ID_PRODUCTO == aux.ID_PRODUCTO 
                                 select CANTIDAD_PRODUCTO.ID_CANTIDAD_PRODUCTO).ToList().FirstOrDefault();
                }
            }

            CANTIDAD_PRODUCTO cANTIDAD_PRODUCTO = await db.CANTIDAD_PRODUCTO.FindAsync(seleccion);
            if (cANTIDAD_PRODUCTO == null)
            {
                return NotFound();
            }

            db.CANTIDAD_PRODUCTO.Remove(cANTIDAD_PRODUCTO);
            await db.SaveChangesAsync();
            
            var carrito = from CANTIDAD_PRODUCTO in db.CANTIDAD_PRODUCTO
                          join PRODUCTO in db.PRODUCTO on CANTIDAD_PRODUCTO.ID_PRODUCTO equals PRODUCTO.ID_PRODUCTO
                          join FOTOS_PRODUCTOS in db.FOTOS_PRODUCTOS on PRODUCTO.ID_PRODUCTO equals FOTOS_PRODUCTOS.ID_PRODUCTO
                          join MULTIMEDIA in db.MULTIMEDIA on FOTOS_PRODUCTOS.ID_MULTIMEDIA equals MULTIMEDIA.ID_MULTIMEDIA
                          where CANTIDAD_PRODUCTO.ID_CARRITO == id_carrito.ToList().First() //cambiar por el id de carrito del usuario
                          select new { PRODUCTO.ID_PRODUCTO, PRODUCTO.NOMBRE_PRODUCTO, PRODUCTO.PRECIO_VENTA, MULTIMEDIA.LINK_MULTIMEDIA};
            var cantidad = carrito.ToList().Count();
            return Ok( new { carrito, cantidad });

            

        }


        // GET: api/CARRITOs


        [Route("api/carritos/")]
        [Authorize]
        [HttpGet]
        public async Task<IHttpActionResult> carrito_page()
        {
            string id_usuario = User.Identity.GetUserId();
            var id_carrito = from CARRITO in db.CARRITO
                             where CARRITO.ID_USUARIO == id_usuario && CARRITO.TIPO_CARRITO == 1
                             select CARRITO.ID_CARRITO;



            
                             

           //consigo el id del carrito ORDENADO POR FECHA DE CREACION

            var carrito = from CANTIDAD_PRODUCTO in db.CANTIDAD_PRODUCTO
                          join PRODUCTO in db.PRODUCTO on CANTIDAD_PRODUCTO.ID_PRODUCTO equals PRODUCTO.ID_PRODUCTO
                          join FOTOS_PRODUCTOS in db.FOTOS_PRODUCTOS on PRODUCTO.ID_PRODUCTO equals FOTOS_PRODUCTOS.ID_PRODUCTO
                          join MULTIMEDIA in db.MULTIMEDIA on FOTOS_PRODUCTOS.ID_MULTIMEDIA equals MULTIMEDIA.ID_MULTIMEDIA
                          where CANTIDAD_PRODUCTO.ID_CARRITO == id_carrito.FirstOrDefault()
                          select new { PRODUCTO.ID_PRODUCTO, PRODUCTO.NOMBRE_PRODUCTO, PRODUCTO.PRECIO_VENTA, MULTIMEDIA.LINK_MULTIMEDIA, CANTIDAD_PRODUCTO.CANTIDAD_PRODUCTO_CARRITO };

            //traigo los productos que pertencen a ese carrito

            var cantidad = carrito.ToList().Count();
            decimal monto = 0;
            if (cantidad > 0)
            {
                foreach(var product in carrito)
                {
                    monto = monto + (product.PRECIO_VENTA).Value;
                }
            }

            return Ok(new { carrito, cantidad, monto });
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

      /*  // POST: api/CARRITOs
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
        }*/

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