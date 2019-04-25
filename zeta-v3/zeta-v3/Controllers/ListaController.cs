using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using zeta_v3.Models;

namespace zeta_v3.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ListaController : ApiController
    {
        //crear
        private zeta_bdEntities12 db = new zeta_bdEntities12();

        [Route("api/lista/crear")]
        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> crear_lista(AuxModel.lista_nueva aux)
        {
            string id_usuario = User.Identity.GetUserId();
            LISTA_DESEOS _DESEOS = new LISTA_DESEOS();
            _DESEOS.NOMBRE_LISTA = aux.NOMBRE_LISTA;
            _DESEOS.ID_USUARIO = id_usuario;

            db.LISTA_DESEOS.Add(_DESEOS);
            await db.SaveChangesAsync();

            return Ok(new { _DESEOS.ID_LISTA_DESEOS, _DESEOS.NOMBRE_LISTA });
        }

        [Route("api/lista/add")]
        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> add_lista(LISTAXPRODUCTO producto_a_lista)
        {
            string id_usuario = User.Identity.GetUserId();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LISTAXPRODUCTO.Add(producto_a_lista);
            await db.SaveChangesAsync();


            return Ok(new { producto_a_lista.ID_LISTA_DESEOS, producto_a_lista.ID_PRODUCTO, producto_a_lista.ID_TAMANO, producto_a_lista.ID_COLOR });

        }

        //retorna productos de una lista
        [Route("api/lista/{id_lista}")]
        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> productos_lista(decimal id_lista)
        {
            var _productos = from LISTAXPRODUCTO in db.LISTAXPRODUCTO
                             join PRODUCTO in db.PRODUCTO on LISTAXPRODUCTO.ID_PRODUCTO equals PRODUCTO.ID_PRODUCTO
                             join COLOR in db.COLOR on LISTAXPRODUCTO.ID_COLOR equals COLOR.ID_COLOR
                             join TAMANO in db.TAMANO on LISTAXPRODUCTO.ID_TAMANO equals TAMANO.ID_TAMANO
                             join LISTA_DESEOS in db.LISTA_DESEOS on LISTAXPRODUCTO.ID_LISTA_DESEOS equals LISTA_DESEOS.ID_LISTA_DESEOS
                             select new
                             {
                                 LISTA_DESEOS.ID_LISTA_DESEOS,
                                 LISTA_DESEOS.NOMBRE_LISTA,
                                 PRODUCTO.ID_PRODUCTO,
                                 PRODUCTO.NOMBRE_PRODUCTO,
                                 PRODUCTO.PRECIO_VENTA,
                                 COLOR.ID_COLOR,
                                 COLOR.NOMBRE_COLOR,
                                 TAMANO.ID_TAMANO,
                                 TAMANO.NOMBRE_TAMANO
                             };

            return Ok(_productos);
        }
        [Route("api/lista/{id_lista}/{id_producto}/{id_color}/{id_tamano}")]
        [HttpDelete]
        [Authorize]
        public IHttpActionResult DeleteProductoLista(decimal id_lista, decimal id_producto,decimal id_color, decimal id_tamano)
        {

            var lista = from LISTAXPRODUCTO in db.LISTAXPRODUCTO
                                   where LISTAXPRODUCTO.ID_COLOR == id_color && LISTAXPRODUCTO.ID_LISTA_DESEOS == id_lista && LISTAXPRODUCTO.ID_PRODUCTO == id_producto && LISTAXPRODUCTO.ID_TAMANO == id_tamano
                                   select LISTAXPRODUCTO;
                                 
            if (lista == null)
            {
                return NotFound();
            }

            db.LISTAXPRODUCTO.Remove(lista.First());
            
            db.SaveChanges();

            return Ok();
        }

        [Route("api/lista/{id_lista}")]
        [HttpDelete]
        [Authorize]
        public IHttpActionResult DeleteLista(decimal id_lista)
        {

            LISTA_DESEOS _DESEOS = db.LISTA_DESEOS.Find(id_lista);

            if (_DESEOS == null)
            {
                return NotFound();
            }
            var productos = from LISTAXPRODUCTO in db.LISTAXPRODUCTO
                            where LISTAXPRODUCTO.ID_LISTA_DESEOS == id_lista
                            select LISTAXPRODUCTO;

            if (productos.ToList().Count() > 0)
            {
                foreach (LISTAXPRODUCTO eliminar in productos)
                {
                    db.LISTAXPRODUCTO.Remove(eliminar);
                }
            }

            db.LISTA_DESEOS.Remove(_DESEOS);

                   

            db.SaveChanges();

            return Ok();
        }
    }
        
}
