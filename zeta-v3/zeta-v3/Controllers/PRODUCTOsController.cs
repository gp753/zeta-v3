using Microsoft.AspNet.Identity;
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

namespace zeta_v3.Controllers
{
    public class PRODUCTOsController : ApiController
    {
        private const string V = "Creado con exito";
        private zeta_bdEntities2 db = new zeta_bdEntities2();
        /*
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
        */
        //productos que pertenecen al usuario (para vender)
        [Route ("api/productos_usuario")]
        [Authorize]
        public async Task<IHttpActionResult> GetPRODUCTO()
        {
            string id_usr = User.Identity.GetUserId();
            var product = (from p in db.INGRESO_PRODUCTO
                           join a in db.PRODUCTO on p.ID_PRODUCTO equals a.ID_PRODUCTO
                           where p.ID_USUARIO == id_usr
                           select a);
            return Ok(product);
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
        [Authorize]
        [ResponseType(typeof(PRODUCTO))]
        public async Task<IHttpActionResult> PostPRODUCTO(PRODUCTO pRODUCTO)
        {
            string id_usr = User.Identity.GetUserId();

            USUARIO usr = db.USUARIO.Find(id_usr);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PRODUCTO.Add(pRODUCTO);
            await db.SaveChangesAsync();
            INGRESO_PRODUCTO productoxusuario = new INGRESO_PRODUCTO();
            productoxusuario.ID_PRODUCTO = pRODUCTO.ID_PRODUCTO;
            productoxusuario.ID_USUARIO = id_usr;
            productoxusuario.CANTIDAD_INGRESO_PRODUCTO = 0;

            db.INGRESO_PRODUCTO.Add(productoxusuario);
            await db.SaveChangesAsync();


            //return CreatedAtRoute("DefaultApi", new { id = pRODUCTO.ID_PRODUCTO }, pRODUCTO);
            return Ok(pRODUCTO.ID_PRODUCTO);
            //preguntarle a marco que quiere que le retorne aca
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