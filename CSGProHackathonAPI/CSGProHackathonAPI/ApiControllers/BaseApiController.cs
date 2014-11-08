using CSGProHackathonAPI.Infrastructure;
using CSGProHackathonAPI.Shared.Data;
using CSGProHackathonAPI.Shared.Infrastructure;
using CSGProHackathonAPI.Shared.Models;
using CSGProHackathonAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace CSGProHackathonAPI.ApiControllers
{
    public class BaseApiController<TModelType> : ApiController where TModelType : BaseModel
    {
        protected User GetCurrentUser()
        {
            User user = null;

            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                var userIdentity = Thread.CurrentPrincipal.Identity as UserIdentity;
                if (userIdentity != null)
                {
                    user = userIdentity.User;
                }
            }

            return user;
        }

        protected void ValidateViewModel(BaseViewModel<TModelType> viewModel, Repository repository, User user)
        {
            // If we have validation messages...
            // then add each of the messages to the model state.
            var validationMessages = viewModel.GetValidationMessages(repository, user);
            if (validationMessages.Count > 0)
            {
                foreach (var validationMessage in validationMessages)
                {
                    ModelState.AddModelError(validationMessage.Key, validationMessage.Message);
                }
            }
        }

        protected ErrorActionResult Error(ModelStateDictionary modelState)
        {
            return new ErrorActionResult(Request, modelState);
        }

        protected ForbiddenActionResult Forbidden(string message)
        {
            return new ForbiddenActionResult(Request, message);
        }

        protected NoContentActionResult NoContent()
        {
            return new NoContentActionResult(Request);
        }

        protected ExcelActionResult<T> Excel<T>(IEnumerable<T> data, string fileName)
        {
            return new ExcelActionResult<T>(data, fileName);
        }
    }
}