using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CSGProHackathonAPI.Infrastructure
{
    public class ExcelActionResult<T> 
        : IHttpActionResult
    {
        private IEnumerable<T> _data;
        private string _fileName;

        public ExcelActionResult(IEnumerable<T> data, string fileName)
        {
            _data = data;
            _fileName = fileName;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var dataType = typeof(T);
            var properties = dataType.GetProperties();

            // TODO get column headers

            using (var excelPackage = new ExcelPackage())
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("Tasks");
                var row = 1;
                var column = 1;

                foreach (var item in _data)
                {
                    foreach (var property in properties)
                    {
                        var value = property.GetValue(item);
                        worksheet.Cells[row, column].Value = value;

                        // TODO format the cell

                        column++;
                    }

                    column = 1;
                    row++;
                }

                excelPackage.Save();

                var stream = excelPackage.Stream;
                stream.Position = 0;

                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(stream)
                };

                var contentType = "application/octet-stream";
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = _fileName
                };

                return Task.FromResult(response);
            }
        }
    }
}