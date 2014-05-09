using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CSGProHackathonAPI.Infrastructure
{
    public class ForbiddenActionResult : IHttpActionResult
    {
        public HttpRequestMessage Request { get; private set; }
        public string Message { get; private set; }

        public ForbiddenActionResult(HttpRequestMessage request, string message)
        {
            this.Request = request;
            this.Message = message;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(ExecuteResult());
        }

        public HttpResponseMessage ExecuteResult()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            response.Content = new StringContent(Message);
            response.RequestMessage = Request;

            return response;
        }
    }
}