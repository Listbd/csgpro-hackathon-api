using CSGProHackathonAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CSGProHackathonAPI.ApiControllers
{
    [BasicAuthorize]
    [EnableCors("*", "*", "*")]
    public class ErrorsController : ApiController
    {
        // GET api/errors
        public IHttpActionResult Get()
        {
            try
            {
                throw new ApplicationException("Testing exception handling.");
            }
            catch (Exception exc)
            {
                return InternalServerError(exc);
            }
        }
    }
}
