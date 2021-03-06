﻿using CSGProHackathonAPI.Infrastructure;
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
    public class ProjectRolesController : BaseApiController<ProjectRole>
    {
        private IRepository _repository;

        public ProjectRolesController(IRepository repository)
        {
            _repository = repository;
        }

        // GET api/projectroles/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var projectRole = _repository.GetProjectRole(id);
                if (projectRole == null)
                {
                    return NotFound();
                }

                var currentUser = GetCurrentUser();
                if (projectRole.Project.UserId != currentUser.UserId)
                {
                    return Forbidden("The current user does not have access to the requested resource.");
                }

                return Ok(projectRole);
            }
            catch (Exception exc)
            {
                return InternalServerError(exc);
            }
        }

        // POST api/projectroles
        public IHttpActionResult Post([FromBody]ProjectRoleStandaloneViewModel viewModel)
        {
            try
            {
                if (viewModel == null)
                {
                    return BadRequest(ModelState);
                }

                var currentUser = GetCurrentUser();

                ValidateViewModel(viewModel, _repository, currentUser);

                if (ModelState.IsValid)
                {
                    var projectRole = viewModel.GetModel(currentUser);

                    _repository.SaveProjectRole(projectRole);

                    var uriString = Url.Link("DefaultApi", new { controller = "ProjectRoles", id = projectRole.ProjectRoleId });

                    return Created(uriString, new { ProjectRoleId = projectRole.ProjectRoleId, Name = projectRole.Name });
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

        // PUT api/projectroles/5
        public IHttpActionResult Put(int id, [FromBody]ProjectRoleStandaloneViewModel viewModel)
        {
            try
            {
                if (viewModel == null)
                {
                    return BadRequest(ModelState);
                }

                var projectRole = _repository.GetProjectRole(id);

                var currentUser = GetCurrentUser();
                if (projectRole.Project.UserId != currentUser.UserId)
                {
                    return Forbidden("You can only update project roles for the current user.");
                }

                ValidateViewModel(viewModel, _repository, currentUser);

                if (ModelState.IsValid)
                {
                    viewModel.UpdateModel(projectRole, currentUser);

                    _repository.SaveProjectRole(projectRole);

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

        // DELETE api/projectroles/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var projectRole = _repository.GetProjectRole(id);

                var currentUser = GetCurrentUser();
                if (projectRole.Project.UserId != currentUser.UserId)
                {
                    return Forbidden("You can only delete project roles for the current user.");
                }

                _repository.DeleteProjectRole(projectRole);

                return NoContent();
            }
            catch (Exception exc)
            {
                return InternalServerError(exc);
            }
        }
    }
}
