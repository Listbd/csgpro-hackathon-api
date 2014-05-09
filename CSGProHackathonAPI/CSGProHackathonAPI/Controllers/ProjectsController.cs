using CSGProHackathonAPI.Shared.Data;
using CSGProHackathonAPI.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CSGProHackathonAPI.Controllers
{
    public class ProjectsController : ApiController
    {
        private Repository _repository;

        public ProjectsController()
        {
            _repository = new Repository();
        }

        // GET api/projects
        public IEnumerable<Project> Get()
        {
            return _repository.GetProjects(1);
        }

        // GET api/projects/5
        public IHttpActionResult Get(int id)
        {
            var project = _repository.GetProject(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        // POST api/projects
        public IHttpActionResult Post([FromBody]Project project)
        {
            _repository.SaveProject(project);

            var uriString = Url.Link("DefaultApi", new { controller = "Projects", id = project.ProjectId });

            return Created(uriString, project);
        }

        // PUT api/projects/5
        public void Put(int id, [FromBody]Project value)
        {
            _repository.SaveProject(value);
        }

        // DELETE api/projects/5
        public void Delete(int id)
        {
            _repository.DeleteProject(id);
        }
    }
}
