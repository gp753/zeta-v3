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
    public class LocalidadesController : ApiController
    {
        private zeta_bdEntities9 db = new zeta_bdEntities9();

        [Route("api/localidades/departamento")]
        public async Task<IHttpActionResult> crear_departamento(DEPARTAMENTO dEPARTAMENTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DEPARTAMENTO.Add(dEPARTAMENTO);
            
            await db.SaveChangesAsync();

            return Created("DefaultApi", new { dEPARTAMENTO.ID_DEPARTAMENTO, dEPARTAMENTO.NOMBRE_DEPARTAMENTO });
        }

        [Route("api/localidades/departamentos")]
        [HttpGet]
        public async Task<IHttpActionResult> get_departamentos()
        {
            var departamentos = from DEPARTAMENTO in db.DEPARTAMENTO
                                select new { DEPARTAMENTO.ID_DEPARTAMENTO, DEPARTAMENTO.NOMBRE_DEPARTAMENTO };

            return Ok(departamentos);
        }

        [Route("api/localidades/ciudad")]
        public async Task<IHttpActionResult> crear_ciudad(CIUDAD cIUDAD)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CIUDAD.Add(cIUDAD);

            await db.SaveChangesAsync();

            return Created("DefaultApi", new { cIUDAD.ID_CIUDAD, cIUDAD.NOMBRE_CIUDAD, cIUDAD.COSTO_DELIVERY_CIUDAD });
        }

        [Route("api/localidades/ciudades")]
        [HttpGet]
        public async Task<IHttpActionResult> get_ciudades()
        {
            

            var citys = from CIUDAD in db.CIUDAD
                        join DEPARTAMENTO in db.DEPARTAMENTO on CIUDAD.ID_DEPARTAMENTO equals DEPARTAMENTO.ID_DEPARTAMENTO
                        select new { CIUDAD.ID_CIUDAD, CIUDAD.NOMBRE_CIUDAD, DEPARTAMENTO.NOMBRE_DEPARTAMENTO, CIUDAD.COSTO_DELIVERY_CIUDAD };

            return Ok(citys);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CIUDADExists(decimal id)
        {
            return db.CIUDAD.Count(e => e.ID_CIUDAD == id) > 0;
        }
    }
}