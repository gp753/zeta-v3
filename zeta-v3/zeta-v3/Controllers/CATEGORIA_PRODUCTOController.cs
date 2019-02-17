﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using zeta_v3.Models;

namespace zeta_v3.Controllers
{
    public class CATEGORIA_PRODUCTOController : ApiController
    {
        private zeta_bdEntities5 db = new zeta_bdEntities5();

        // GET: api/CATEGORIA_PRODUCTO
        public IQueryable<CATEGORIA_PRODUCTO> GetCATEGORIA_PRODUCTO()
        {
            return db.CATEGORIA_PRODUCTO;
        }

        // GET: api/CATEGORIA_PRODUCTO/5
        [ResponseType(typeof(CATEGORIA_PRODUCTO))]
        public IHttpActionResult GetCATEGORIA_PRODUCTO(decimal id)
        {
            CATEGORIA_PRODUCTO cATEGORIA_PRODUCTO = db.CATEGORIA_PRODUCTO.Find(id);
            if (cATEGORIA_PRODUCTO == null)
            {
                return NotFound();
            }

            return Ok(cATEGORIA_PRODUCTO);
        }

        // PUT: api/CATEGORIA_PRODUCTO/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCATEGORIA_PRODUCTO(decimal id, CATEGORIA_PRODUCTO cATEGORIA_PRODUCTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cATEGORIA_PRODUCTO.ID_CATEGORIA)
            {
                return BadRequest();
            }

            db.Entry(cATEGORIA_PRODUCTO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CATEGORIA_PRODUCTOExists(id))
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

        // POST: api/CATEGORIA_PRODUCTO
        [ResponseType(typeof(CATEGORIA_PRODUCTO))]
        public IHttpActionResult PostCATEGORIA_PRODUCTO(CATEGORIA_PRODUCTO cATEGORIA_PRODUCTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            db.CATEGORIA_PRODUCTO.Add(cATEGORIA_PRODUCTO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cATEGORIA_PRODUCTO.ID_CATEGORIA },new { cATEGORIA_PRODUCTO.ID_CATEGORIA, cATEGORIA_PRODUCTO.NOMBRE_CATEGORIA });
          
        }

        // DELETE: api/CATEGORIA_PRODUCTO/5
        [ResponseType(typeof(CATEGORIA_PRODUCTO))]
        public IHttpActionResult DeleteCATEGORIA_PRODUCTO(decimal id)
        {
            CATEGORIA_PRODUCTO cATEGORIA_PRODUCTO = db.CATEGORIA_PRODUCTO.Find(id);
            if (cATEGORIA_PRODUCTO == null)
            {
                return NotFound();
            }

            db.CATEGORIA_PRODUCTO.Remove(cATEGORIA_PRODUCTO);
            db.SaveChanges();

            return Ok(cATEGORIA_PRODUCTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CATEGORIA_PRODUCTOExists(decimal id)
        {
            return db.CATEGORIA_PRODUCTO.Count(e => e.ID_CATEGORIA == id) > 0;
        }
    }
}