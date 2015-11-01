using CSGProHackathonAPI.Infrastructure;
using CSGProHackathonAPI.Shared.Data;
using CSGProHackathonAPI.Shared.Models;
using CSGProHackathonAPI.Shared.UtilityModels;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CSGProHackathonAPI.ApiControllers
{
    [BasicAuthorize]
    [EnableCors("*", "*", "*")]
    public class ProjectHoursController : BaseApiController<Project>
    {
        public enum Format
        {
            Json,
            Excel
        }

        private IRepository _repository;

        public ProjectHoursController(IRepository repository)
        {
            _repository = repository;
        }

        // GET api/projecthours?format={value}
        public IHttpActionResult Get(Format format = Format.Json)
        {
            var currentUser = GetCurrentUser();

            var dateUtcStart = currentUser.ConvertLocalTimeToUtc(SqlDateTime.MinValue.Value);
            var dateUtcEnd = currentUser.ConvertLocalTimeToUtc(SqlDateTime.MaxValue.Value);

            return GetProjects(currentUser.UserId, dateUtcStart, dateUtcEnd, format);
        }

        // GET api/projecthours?dateStart={value}&dateEnd={value}&format={value}
        public IHttpActionResult Get(DateTime dateStart, DateTime dateEnd, Format format = Format.Json)
        {
            var currentUser = GetCurrentUser();

            var dateUtcStart = currentUser.ConvertLocalTimeToUtc(dateStart);
            var dateUtcEnd = currentUser.ConvertLocalTimeToUtc(dateEnd);

            return GetProjects(currentUser.UserId, dateUtcStart, dateUtcEnd, format);
        }

        private IHttpActionResult GetProjects(int currentUserId, DateTime dateUtcStart, DateTime dateUtcEnd, Format format)
        {
            var projects = _repository.GetProjectHours(currentUserId, dateUtcStart, dateUtcEnd);

            switch (format)
            {
                case Format.Json:
                    return Ok(projects);
                case Format.Excel:
                    return Excel(Project.GetProjectTasksForExcel(projects), "Temp.xlsx");
                default:
                    throw new ApplicationException("Unexpected Format enum value: " + format.ToString());
            }
        }
    }
}