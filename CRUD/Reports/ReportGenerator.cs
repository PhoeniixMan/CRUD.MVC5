using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using CRUD.Models;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace CRUD.Reports
{
    public class ReportGenerator : ActionResult
    {
        private Stream inputStream = null;
        private readonly byte[] contentBytes;

        public ReportGenerator(string path, List<Employee> dataSet )
        {
            ReportDocument reportDocument = new ReportDocument();
            reportDocument.Load(path);
            reportDocument.SetDataSource(dataSet);
            contentBytes = StreamToBytes(reportDocument.ExportToStream(ExportFormatType.PortableDocFormat));
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.ApplicationInstance.Response;
            response.Clear();
            response.Buffer = false;
            response.ClearContent();
            response.ClearHeaders();
            response.Cache.SetCacheability(HttpCacheability.Public);
            response.ContentType = "application/pdf";

            using (var stream = new MemoryStream(contentBytes))
            {
                stream.WriteTo(response.OutputStream);
                stream.Flush();
            }
           
        }

        private static byte[] StreamToBytes(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}