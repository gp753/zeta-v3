using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using zeta_v3.Models;

namespace zeta_v3.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class COLORsController : ApiController
    {
        private zeta_bdEntities7 db = new zeta_bdEntities7();

        // GET: api/COLORs
        public IQueryable<COLOR> GetCOLOR()
        {
            return db.COLOR;
        }

        // GET: api/COLORs/5
        [ResponseType(typeof(COLOR))]
        public IHttpActionResult GetCOLOR(decimal id)
        {
            COLOR cOLOR = db.COLOR.Find(id);
            if (cOLOR == null)
            {
                return NotFound();
            }

            return Ok(cOLOR);
        }

        // PUT: api/COLORs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCOLOR(decimal id, COLOR cOLOR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cOLOR.ID_COLOR)
            {
                return BadRequest();
            }

            db.Entry(cOLOR).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!COLORExists(id))
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

        // POST: api/COLORs
        [ResponseType(typeof(COLOR))]
        public IHttpActionResult PostCOLOR(COLOR cOLOR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.COLOR.Add(cOLOR);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cOLOR.ID_COLOR }, cOLOR);
        }

        // DELETE: api/COLORs/5
        [ResponseType(typeof(COLOR))]
        public IHttpActionResult DeleteCOLOR(decimal id)
        {
            COLOR cOLOR = db.COLOR.Find(id);
            if (cOLOR == null)
            {
                return NotFound();
            }

            db.COLOR.Remove(cOLOR);
            db.SaveChanges();

            return Ok(cOLOR);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool COLORExists(decimal id)
        {
            return db.COLOR.Count(e => e.ID_COLOR == id) > 0;
        }
    }
}