using CSGProHackathonAPI.Shared.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace CSGProHackathonAPI.Infrastructure
{
    public class ErrorActionResult : IHttpActionResult
    {
        public HttpRequestMessage Request { get; private set; }
        public ModelStateDictionary ModelState { get; private set; }

        public ErrorActionResult(HttpRequestMessage request, ModelStateDictionary modelState)
        {
            this.Request = request;
            this.ModelState = modelState;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(ExecuteResult());
        }

        public HttpResponseMessage ExecuteResult()
        {
            var modelState = ModelState;
            var errors = new List<ValidationMessage>();

            foreach (var key in ModelState.Keys)
            {
                var errorKey = key;
                if (errorKey.IndexOf("viewModel.") != -1)
                {
                    errorKey = errorKey.Replace("viewModel.", string.Empty);
                }

                var modelStateKey = modelState[key];
                foreach (var error in modelStateKey.Errors)
                {
                    errors.Add(new ValidationMessage(errorKey, error.ErrorMessage));
                }
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, 
                new
                {
                    Message = "The following errors were found with the submitted data.", 
                    Errors = errors
                });
        }
    }
}