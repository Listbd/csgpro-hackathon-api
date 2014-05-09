using CSGProHackathonAPI.Infrastructure;
using CSGProHackathonAPI.Shared.Data;
using CSGProHackathonAPI.Shared.Infrastructure;
using CSGProHackathonAPI.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CSGProHackathonAPI.ApiControllers
{
    [BasicAuthorize]
    public class ProjectsController : BaseApiController
    {
        private Repository _repository;

        public ProjectsController()
        {
            _repository = new Repository();
        }

        // GET api/projects
        public IEnumerable<Project> Get()
        {
            var userIdentity = (UserIdentity)User.Identity;

            return _repository.GetProjects(userIdentity.User.UserId);
        }

        // GET api/projects/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var project = _repository.GetProject(id);
                if (project == null)
                {
                    return NotFound();
                }

                var userIdentity = (UserIdentity)User.Identity;
                if (project.UserId != userIdentity.User.UserId)
                {
                    return Forbidden("The current user does not have access to the requested resource.");
                }

                return Ok(project);
            }
            catch (Exception exc)
            {
                return InternalServerError(exc);
            }
        }

        // POST api/projects
        public IHttpActionResult Post([FromBody]Project project)
        {
            try
            {
                var userIdentity = (UserIdentity)User.Identity;
                if (project.UserId != userIdentity.User.UserId)
                {
                    return Forbidden("You can only add projects for the current user.");
                }

                _repository.SaveProject(project);

                var uriString = Url.Link("DefaultApi", new { controller = "Projects", id = project.ProjectId });

                return Created(uriString, project);
            }
            catch (Exception exc)
            {
                return InternalServerError(exc);
            }
        }

        // PUT api/projects/5
        public IHttpActionResult Put(int id, [FromBody]Project project)
        {
            try
            {
                var userIdentity = (UserIdentity)User.Identity;
                if (project.UserId != userIdentity.User.UserId)
                {
                    return Forbidden("You can only update projects for the current user.");
                }

                _repository.SaveProject(project);

                return NoContent();
            }
            catch (Exception exc)
            {
                return InternalServerError(exc);
            }
        }

        // DELETE api/projects/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var project = _repository.GetProject(id);

                var userIdentity = (UserIdentity)User.Identity;
                if (project.UserId != userIdentity.User.UserId)
                {
                    return Forbidden("You can only delete projects for the current user.");
                }

                _repository.DeleteProject(project);

                return NoContent();
            }
            catch (Exception exc)
            {
                return InternalServerError(exc);
            }
        }
    }
}
