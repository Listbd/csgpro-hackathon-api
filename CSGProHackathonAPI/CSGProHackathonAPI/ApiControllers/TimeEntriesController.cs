using CSGProHackathonAPI.Infrastructure;
using CSGProHackathonAPI.Shared.Data;
using CSGProHackathonAPI.Shared.Models;
using CSGProHackathonAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CSGProHackathonAPI.ApiControllers
{
    [BasicAuthorize]
    [EnableCors("*", "*", "*")]
    public class TimeEntriesController : BaseApiController<TimeEntry>
    {
        private Repository _repository;

        public TimeEntriesController()
        {
            _repository = new Repository();
        }

        // GET api/timeentries
        public IEnumerable<TimeEntry> Get()
        {
            var currentUser = GetCurrentUser();
            var date = currentUser.ConvertUtcToLocalTime(DateTime.UtcNow).Date;

            return _repository.GetTimeEntries(date, currentUser);
        }

        // GET api/timeentries
        [Route("api/timeentries/date/{date}")]
        public IEnumerable<TimeEntry> Get([FromUri]DateTime date)
        {
            var currentUser = GetCurrentUser();

            return _repository.GetTimeEntries(date, currentUser);
        }

        // GET api/timeentries/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var timeEntry = _repository.GetTimeEntry(id);
                if (timeEntry == null)
                {
                    return NotFound();
                }

                var currentUser = GetCurrentUser();
                if (timeEntry.UserId != currentUser.UserId)
                {
                    return Forbidden("The current user does not have access to the requested resource.");
                }

                return Ok(timeEntry);
            }
            catch (Exception exc)
            {
                return InternalServerError(exc);
            }
        }

        // POST api/timeentries
        public IHttpActionResult Post([FromBody]TimeEntryViewModel viewModel)
        {
            try
            {
                var currentUser = GetCurrentUser();

                ValidateViewModel(viewModel, _repository, currentUser);

                if (ModelState.IsValid)
                {
                    var timeEntry = viewModel.GetModel(currentUser);

                    _repository.SaveTimeEntry(timeEntry, currentUser);

                    var uriString = Url.Link("DefaultApi", new { controller = "TimeEntries", id = timeEntry.TimeEntryId });

                    return Created(uriString, new { TimeEntryId = timeEntry.TimeEntryId });
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

        // PUT api/timeentries/5
        public IHttpActionResult Put(int id, [FromBody]TimeEntryViewModel viewModel)
        {
            try
            {
                var timeEntry = _repository.GetTimeEntry(id);

                var currentUser = GetCurrentUser();
                if (timeEntry.UserId != currentUser.UserId)
                {
                    return Forbidden("You can only update time entries for the current user.");
                }

                ValidateViewModel(viewModel, _repository, currentUser);

                if (ModelState.IsValid)
                {
                    viewModel.UpdateModel(timeEntry, currentUser);

                    _repository.SaveTimeEntry(timeEntry, currentUser);

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

        // DELETE api/timeentries/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var timeEntry = _repository.GetTimeEntry(id);

                var currentUser = GetCurrentUser();
                if (timeEntry.UserId != currentUser.UserId)
                {
                    return Forbidden("You can only delete time entries for the current user.");
                }

                _repository.DeleteTimeEntry(timeEntry);

                return NoContent();
            }
            catch (Exception exc)
            {
                return InternalServerError(exc);
            }
        }
    }
}