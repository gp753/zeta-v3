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
    
    public class CheckoutController : ApiController
    {
        private zeta_bdEntities12 db = new zeta_bdEntities12();

        [Route("api/checkout")]
        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> checkout()
        {
            string id_usuario = User.Identity.GetUserId();
            var id_carrito = from CARRITO in db.CARRITO
                             where CARRITO.ID_USUARIO == id_usuario && CARRITO.TIPO_CARRITO == 1
                             select CARRITO.ID_CARRITO;

            CHECKOUT cHECKOUT = new CHECKOUT();
            cHECKOUT.ID_CARRITO = id_carrito.First() ;
            cHECKOUT.FECHA_CHECKOUT = DateTime.Now;
            db.CHECKOUT.Add(cHECKOUT);
            await db.SaveChangesAsync();

            var productos_carrito = from CANTIDAD_PRODUCTO in db.CANTIDAD_PRODUCTO
                                    where CANTIDAD_PRODUCTO.ID_CARRITO == id_carrito.First()
                                    select CANTIDAD_PRODUCTO;
            
            
            foreach (CANTIDAD_PRODUCTO _PRODUCTO in productos_carrito)
            {
               await producto_a_facturaAsync(_PRODUCTO); //se genera el producto como estado pendiente para el usuario y para el vendedor
            }
            CARRITO cARRITO = await db.CARRITO.FindAsync(cHECKOUT.ID_CARRITO);
            cARRITO.TIPO_CARRITO = 2;
            cARRITO.ID_CHECKOUT = cHECKOUT.ID_CHECKOUT;
            CARRITO cARRITO_nuevo = new CARRITO();
            cARRITO_nuevo.ID_USUARIO = cARRITO.ID_USUARIO;

            return Ok();
        }

        public async System.Threading.Tasks.Task producto_a_facturaAsync(CANTIDAD_PRODUCTO _PRODUCTO )
        {
            //nota: verificar ofertas, verificar si se puede tener precios diferentes por colores y tamanhos

            PRODUCTO_FACTURA pRODUCTO_FACTURA = new PRODUCTO_FACTURA();
            pRODUCTO_FACTURA.ID_PRODUCTO = _PRODUCTO.ID_PRODUCTO;
            pRODUCTO_FACTURA.ID_TAMANO = _PRODUCTO.ID_TAMANO;
            pRODUCTO_FACTURA.ID_COLOR = _PRODUCTO.ID_COLOR;
            pRODUCTO_FACTURA.CANTIDAD_PRODUCTO_FACTURADO = _PRODUCTO.CANTIDAD_PRODUCTO_CARRITO;
            
            PRODUCTO pRODUCTO = await db.PRODUCTO.FindAsync(_PRODUCTO.ID_PRODUCTO);
            pRODUCTO_FACTURA.PRECIO_PRODUCTO_FACTURA = pRODUCTO.PRECIO_VENTA;
            pRODUCTO_FACTURA.ESTADO = 1;

            db.PRODUCTO_FACTURA.Add(pRODUCTO_FACTURA);
            await db.SaveChangesAsync();



            /*
             1 PENDIENTE DE CONFIRMACION (PROCESANDO)
             2 PREPARANDO
             3 ENVIADO
             4 ENTREGADO
             */
        }
    }
}
