using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using DinkToPdf;
using DinkToPdf.Contracts;


namespace zeta_v3.Controllers
{
    

    public class PDFController : ControllerBase
    {

        private IConverter _converter;

        public PDFController(IConverter converter)
        {
            _converter = converter;
        }

        [System.Web.Http.Route("api/pdf")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult CreatePDF()
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",
                Out = @"C:\reporte.pdf"
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = "<html>Hello world</html>",
                
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            _converter.Convert(pdf);
            

            return Ok("Successfully created PDF document.");

        }

        private IHttpActionResult Ok(string v)
        {
            throw new NotImplementedException();
        }

        protected override void ExecuteCore()
        {
            throw new NotImplementedException();
        }
    }
}
