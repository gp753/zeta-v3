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
    public class CARACTERISTICASController : ApiController
    {
        private zeta_bdEntities7 db = new zeta_bdEntities7();

        // GET: api/CARACTERISTICAS
        public IQueryable<CARACTERISTICAS> GetCARACTERISTICAS()
        {
            return db.CARACTERISTICAS;
        }

        // GET: api/CARACTERISTICAS/5
        public async Task<IHttpActionResult> GetCARACTERISTICAS(decimal id)
        {
       
            var caracteristicas = from CARACTERISTICAS in db.CARACTERISTICAS
                                  where CARACTERISTICAS.ID_CATEGORIA == id
                                  select new { CARACTERISTICAS.ID_CARACTERISTICA, CARACTERISTICAS.NOMBRE_CARACTERISTICA };
            return Ok(caracteristicas);
        }

        // PUT: api/CARACTERISTICAS/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCARACTERISTICAS(decimal id, CARACTERISTICAS cARACTERISTICAS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cARACTERISTICAS.ID_CARACTERISTICA)
            {
                return BadRequest();
            }

            db.Entry(cARACTERISTICAS).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CARACTERISTICASExists(id))
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
       
        // POST: api/CARACTERISTICAS
        [ResponseType(typeof(CARACTERISTICAS))]
        public async Task<IHttpActionResult> PostCARACTERISTICAS(CARACTERISTICAS cARACTERISTICAS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            db.CARACTERISTICAS.Add(cARACTERISTICAS);
            await db.SaveChangesAsync();
            
            return CreatedAtRoute("DefaultApi", new { id = cARACTERISTICAS.ID_CARACTERISTICA }, new { cARACTERISTICAS.ID_CARACTERISTICA, cARACTERISTICAS.NOMBRE_CARACTERISTICA });
        }

        // DELETE: api/CARACTERISTICAS/5
        [ResponseType(typeof(CARACTERISTICAS))]
        public async Task<IHttpActionResult> DeleteCARACTERISTICAS(decimal id)
        {
            CARACTERISTICAS cARACTERISTICAS = await db.CARACTERISTICAS.FindAsync(id);
            if (cARACTERISTICAS == null)
            {
                return NotFound();
            }

            db.CARACTERISTICAS.Remove(cARACTERISTICAS);
            await db.SaveChangesAsync();

            return Ok(cARACTERISTICAS);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CARACTERISTICASExists(decimal id)
        {
            return db.CARACTERISTICAS.Count(e => e.ID_CARACTERISTICA == id) > 0;
        }
    }
}