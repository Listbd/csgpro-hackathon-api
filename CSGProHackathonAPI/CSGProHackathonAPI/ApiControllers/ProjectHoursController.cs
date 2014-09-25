using CSGProHackathonAPI.Infrastructure;
using CSGProHackathonAPI.Shared.Data;
using CSGProHackathonAPI.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;

namespace CSGProHackathonAPI.ApiControllers
{
    [BasicAuthorize]
    [EnableCors("*", "*", "*")]
    public class ProjectHoursController : BaseApiController<Project>
    {
        private Repository _repository;

        public ProjectHoursController()
        {
            _repository = new Repository();
        }

        // GET api/projecthours
        public IEnumerable<Project> Get()
        {
            var currentUser = GetCurrentUser();

            return _repository.GetProjectHours(currentUser.UserId);
        }
    }
}