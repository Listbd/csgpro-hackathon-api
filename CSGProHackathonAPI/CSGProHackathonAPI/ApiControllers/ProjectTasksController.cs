using CSGProHackathonAPI.Infrastructure;
using CSGProHackathonAPI.Shared.Data;
using CSGProHackathonAPI.Shared.Models;
using CSGProHackathonAPI.ViewModels;
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
    public class ProjectTasksController : BaseApiController<ProjectTask>
    {
        private Repository _repository;

        public ProjectTasksController()
        {
            _repository = new Repository();
        }

        // GET api/projecttasks/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var projectTask = _repository.GetProjectTask(id);
                if (projectTask == null)
                {
                    return NotFound();
                }

                var currentUser = GetCurrentUser();
                if (projectTask.Project.UserId != currentUser.UserId)
                {
                    return Forbidden("The current user does not have access to the requested resource.");
                }

                return Ok(projectTask);
            }
            catch (Exception exc)
            {
                return InternalServerError(exc);
            }
        }

        // POST api/projecttasks
        public IHttpActionResult Post([FromBody]ProjectTaskStandaloneViewModel viewModel)
        {
            try
            {
                var currentUser = GetCurrentUser();

                ValidateViewModel(viewModel, _repository, currentUser);

                if (ModelState.IsValid)
                {
                    var projectTask = viewModel.GetModel(currentUser);

                    _repository.SaveProjectTask(projectTask);

                    var uriString = Url.Link("DefaultApi", new { controller = "ProjectTasks", id = projectTask.ProjectTaskId });

                    return Created(uriString, new { ProjectTaskId = projectTask.ProjectTaskId, Name = projectTask.Name });
                }
                else
                {
                    return Error(ModelState);
                }
            }
            catch (Exception exc)
            {
                return InternalServerError(exc);
            }
        }

        // PUT api/projecttasks/5
        public IHttpActionResult Put(int id, [FromBody]ProjectTaskStandaloneViewModel viewModel)
        {
            try
            {
                var projectTask = _repository.GetProjectTask(id);

                var currentUser = GetCurrentUser();
                if (projectTask.Project.UserId != currentUser.UserId)
                {
                    return Forbidden("You can only update project tasks for the current user.");
                }

                ValidateViewModel(viewModel, _repository, currentUser);

                if (ModelState.IsValid)
                {
                    viewModel.UpdateModel(projectTask, currentUser);

                    _repository.SaveProjectTask(projectTask);

                    return NoContent();
                }
                else
                {
                    return Error(ModelState);
                }
            }
            catch (Exception exc)
            {
                return InternalServerError(exc);
            }
        }

        // DELETE api/projecttasks/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var projectTask = _repository.GetProjectTask(id);

                var currentUser = GetCurrentUser();
                if (projectTask.Project.UserId != currentUser.UserId)
                {
                    return Forbidden("You can only delete project tasks for the current user.");
                }

                _repository.DeleteProjectTask(projectTask);

                return NoContent();
            }
            catch (Exception exc)
            {
                return InternalServerError(exc);
            }
        }
    }
}
