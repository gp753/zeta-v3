using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using zeta_v3.Models;

namespace zeta_v3.Controllers
{
    public class MultimediaController : ApiController
    {
        private zeta_bdEntities12 db = new zeta_bdEntities12();

        [Route("api/producto/imagen/{id_producto}/{id_color}/{id_tamano}")]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Post(decimal id_producto, decimal id_color, decimal id_tamano)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {

                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {

                            string nombre = (DateTime.Now).ToString("yyyyMMddHHmmss") + ext;

                            //var filePath = HttpContext.Current.Server.MapPath("~/Userimage/" + postedFile.FileName  );
                            var filePath = HttpContext.Current.Server.MapPath("~/Multimedia/" + nombre);

                            postedFile.SaveAs(filePath);
                           string link =  "http://apizeta.harasgb.com/Multimedia/" + nombre;

                            MULTIMEDIA mULTIMEDIA = new MULTIMEDIA();
                            mULTIMEDIA.FECHA_CARGA = DateTime.Now;
                            mULTIMEDIA.LINK_MULTIMEDIA = link;
                            db.MULTIMEDIA.Add(mULTIMEDIA);


                            FOTOS_PRODUCTOS fOTOS = new FOTOS_PRODUCTOS();
                            fOTOS.ID_PRODUCTO = id_producto;
                            if (id_color > 0)
                            {
                                fOTOS.ID_COLOR = id_color;
                            }
                            if (id_tamano > 0)
                            {
                                fOTOS.ID_TAMANO = id_tamano;
                            }
                            
                            fOTOS.ID_MULTIMEDIA = mULTIMEDIA.ID_MULTIMEDIA;
                            db.FOTOS_PRODUCTOS.Add(fOTOS);

                            await db.SaveChangesAsync();
                            
                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                     return Request.CreateErrorResponse(HttpStatusCode.Created, message1);
                    
                }
                var res = string.Format("Please Upload a image.");

                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }

            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", ex);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }

        [Route("api/producto/imagen/{id_publicacion}")]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> PostBanner(decimal id_publicacion)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {

                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {

                            string nombre = (DateTime.Now).ToString("yyyyMMddHHmmss") + ext;

                            //var filePath = HttpContext.Current.Server.MapPath("~/Userimage/" + postedFile.FileName  );
                            var filePath = HttpContext.Current.Server.MapPath("~/Multimedia/" + nombre);

                            postedFile.SaveAs(filePath);
                            string link = "http://apizeta.harasgb.com/Multimedia/" + nombre;

                            PUBLICACION pUBLICACION = await db.PUBLICACION.FindAsync(id_publicacion);

                            if(pUBLICACION == null)
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, "Publicacion no existe!");

                            }

                            pUBLICACION.LINK_IMAGEN_PUBLICACION = link;
                            db.Entry(pUBLICACION).State = EntityState.Modified;

                            await db.SaveChangesAsync();

                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1);

                }
                var res = string.Format("Please Upload a image.");

                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }

            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", ex);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }
    }
}
