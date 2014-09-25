using CSGProHackathonAPI.Infrastructure;
using CSGProHackathonAPI.Shared.Data;
using CSGProHackathonAPI.Shared.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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

            var dateUtcStart = currentUser.ConvertLocalTimeToUtc(SqlDateTime.MinValue.Value);
            var dateUtcEnd = currentUser.ConvertLocalTimeToUtc(SqlDateTime.MaxValue.Value);

            return _repository.GetProjectHours(currentUser.UserId, dateUtcStart, dateUtcEnd);
        }

        // GET api/projecthours?dateStart={value}&dateEnd={value}
        public IEnumerable<Project> Get(DateTime dateStart, DateTime dateEnd)
        {
            var currentUser = GetCurrentUser();

            var dateUtcStart = currentUser.ConvertLocalTimeToUtc(dateStart);
            var dateUtcEnd = currentUser.ConvertLocalTimeToUtc(dateEnd);

            return _repository.GetProjectHours(currentUser.UserId, dateUtcStart, dateUtcEnd);
        }
    }
}