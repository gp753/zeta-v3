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
        private zeta_bdEntities12 db = new zeta_bdEntities12();

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

        [Route("api/usuarios/direcciones")]
        [HttpGet]
        public async Task<IHttpActionResult> Direccion_get()
        {
            //falta devolver la cantidad en stock que hay de cada producto

            
            var dir = from DIRECION in db.DIRECION
                      join CIUDAD in db.CIUDAD on DIRECION.ID_DIRECCION equals CIUDAD.ID_CIUDAD
                      where DIRECION.ID_USUARIO == "1"
                      select new { DIRECION.ID_DIRECCION, DIRECION.CALLE_1, DIRECION.CALLE_2, DIRECION.G_MAP, DIRECION.NRO_CASA, DIRECION.REFERENCIA, CIUDAD.ID_CIUDAD, CIUDAD.NOMBRE_CIUDAD };
            return Ok(dir);
        }

        [Route("api/usuarios/direccion/nueva")]
        [HttpPost]
        public async Task<IHttpActionResult> Direccion_crear(DIRECION dIRECION)
        {
            dIRECION.ID_USUARIO = "1";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DIRECION.Add(dIRECION);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                
                throw;
                
            }



            return CreatedAtRoute("DefaultApi", new { id = dIRECION.ID_DIRECCION }, new { dIRECION.ID_DIRECCION, dIRECION.CALLE_1, dIRECION.CALLE_2, dIRECION.NRO_CASA});
        }

        [Route("api/usuarios/direccion/editar")]
        [HttpPost]
        public async Task<IHttpActionResult> Direccion_editar(DIRECION dIRECION)
        {
           

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(dIRECION).State = EntityState.Modified;
           


            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {

                if(db.DIRECION.Count(e => e.ID_DIRECCION == dIRECION.ID_DIRECCION) > 0)
                {
                    throw;
                }
                else
                {
                    return NotFound();
                }

            }



            return CreatedAtRoute("DefaultApi", new { id = dIRECION.ID_DIRECCION }, new { dIRECION.ID_DIRECCION, dIRECION.CALLE_1, dIRECION.CALLE_2, dIRECION.NRO_CASA });
        }

        [Route("api/usuarios/direccion/eliminar/{id_direccion}")]
        [HttpDelete]
        public async Task<IHttpActionResult> Direccion_eliminar(decimal id_direccion)
        {
            DIRECION dIRECION = await db.DIRECION.FindAsync(id_direccion);

            if(dIRECION == null)
            {
                return NotFound();
            }

            db.DIRECION.Remove(dIRECION);
            await db.SaveChangesAsync();


           return Ok();
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