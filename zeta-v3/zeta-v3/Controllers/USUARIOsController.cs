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
    public class USUARIOsController : ApiController
    {
        private zeta_bdEntities10 db = new zeta_bdEntities10();

        /*// GET: api/USUARIOs
        public IQueryable<USUARIO> GetUSUARIO()
        {
            return db.USUARIO;
        }*/

        [Route("api/usuarios/all")]
        [HttpGet]
        public async Task<IHttpActionResult> usuarios_todos()
        {      

            var users = from USUARIO in db.USUARIO
                        where USUARIO.ESTADO > 0
                        select new { USUARIO.NOMBRE, USUARIO.APELLIDO, USUARIO.EMAIL, USUARIO.FECHA_INGRESO, USUARIO.ID_USUARIO };
            return Ok(users);
        }

        // GET: api/USUARIOs/5
        [ResponseType(typeof(USUARIO))]
        public async Task<IHttpActionResult> GetUSUARIO(string id)
        {
            USUARIO uSUARIO = await db.USUARIO.FindAsync(id);
            if (uSUARIO == null)
            {
                return NotFound();
            }

            return Ok(uSUARIO);
        }

        // PUT: api/USUARIOs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUSUARIO(string id, USUARIO uSUARIO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != uSUARIO.ID_USUARIO)
            {
                return BadRequest();
            }

            db.Entry(uSUARIO).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!USUARIOExists(id))
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

        // POST: api/USUARIOs
        [ResponseType(typeof(USUARIO))]
        public async Task<IHttpActionResult> PostUSUARIO(USUARIO uSUARIO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.USUARIO.Add(uSUARIO);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (USUARIOExists(uSUARIO.ID_USUARIO))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = uSUARIO.ID_USUARIO }, uSUARIO);
        }

        //post para eliminar usuarios
        [Route("api/usuarios/delete/{id_usuario}")]
        public async Task<IHttpActionResult> eliminar_usuario(string id_usuario)
        {
           

            USUARIO uSUARIO = await db.USUARIO.FindAsync(id_usuario);

            if (uSUARIO == null)
            {
                return NotFound();
            }

            uSUARIO.ESTADO = 0;

            db.Entry(uSUARIO).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!USUARIOExists(id_usuario))
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
        // DELETE: api/USUARIOs/5
        [ResponseType(typeof(USUARIO))]
        public async Task<IHttpActionResult> DeleteUSUARIO(string id)
        {
            USUARIO uSUARIO = await db.USUARIO.FindAsync(id);
            if (uSUARIO == null)
            {
                return NotFound();
            }

            db.USUARIO.Remove(uSUARIO);
            await db.SaveChangesAsync();

            return Ok(uSUARIO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool USUARIOExists(string id)
        {
            return db.USUARIO.Count(e => e.ID_USUARIO == id) > 0;
        }
    }
}