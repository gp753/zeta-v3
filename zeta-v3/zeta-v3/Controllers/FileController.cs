using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace zeta_v3.Controllers
{
    public class FileController : ApiController
    {
        // GET: api/File
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/File/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/File
       public HttpResponseMessage Post()
{
    var httpRequest = HttpContext.Current.Request;
    if (httpRequest.Files.Count > 0)
    {
        foreach (string fileName in httpRequest.Files.Keys)
        {
            var file = httpRequest.Files[fileName];
            var filePath = HttpContext.Current.Server.MapPath("~/archivos/" + file.FileName);
            file.SaveAs(filePath);
        }

        return Request.CreateResponse(HttpStatusCode.Created);
    }

    return Request.CreateResponse(HttpStatusCode.BadRequest);
}

        // PUT: api/File/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/File/5
        public void Delete(int id)
        {
        }
    }
}
