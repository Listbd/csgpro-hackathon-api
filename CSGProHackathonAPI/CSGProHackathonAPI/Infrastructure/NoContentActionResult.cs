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
    public class NoContentActionResult : IHttpActionResult
    {
        public HttpRequestMessage Request { get; private set; }

        public NoContentActionResult(HttpRequestMessage request)
        {
            this.Request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(ExecuteResult());
        }

        public HttpResponseMessage ExecuteResult()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NoContent);
            response.RequestMessage = Request;

            return response;
        }
    }
}