using CSGProHackathonAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CSGProHackathonAPI.ApiControllers
{
    public class BaseApiController : ApiController
    {
        protected ForbiddenActionResult Forbidden(string message)
        {
            return new ForbiddenActionResult(Request, message);
        }

        protected NoContentActionResult NoContent()
        {
            return new NoContentActionResult(Request);
        }
    }
}