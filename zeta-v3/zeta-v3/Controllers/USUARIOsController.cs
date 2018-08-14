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
using Microsoft.AspNet.Identity;

namespace zeta_v3.Controllers
{
    public class USUARIOsController : ApiController
    {
        private zeta_bdEntities1 db = new zeta_bdEntities1();

        // GET: api/USUARIOs
        public IQueryable<USUARIO> GetUSUARIO()
        {
            return db.USUARIO;
        }

        // GET: api/USUARIOs/5
        [ResponseType(typeof(USUARIO))]
        public async Task<IHttpActionResult> GetUSUARIO(decimal id)
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
        public async Task<IHttpActionResult> PutUSUARIO(decimal id, USUARIO uSUARIO)
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
            uSUARIO.ESTADO = 1; // sin habilitar
            uSUARIO.FECHA_INGRESO = DateTime.Now;
            db.USUARIO.Add(uSUARIO);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = uSUARIO.ID_USUARIO }, uSUARIO);
        }

        // DELETE: api/USUARIOs/5
        [ResponseType(typeof(USUARIO))]
        public async Task<IHttpActionResult> DeleteUSUARIO(decimal id)
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

        private bool USUARIOExists(decimal id)
        {
            return db.USUARIO.Count(e => e.ID_USUARIO == id) > 0;
        }
    }
}