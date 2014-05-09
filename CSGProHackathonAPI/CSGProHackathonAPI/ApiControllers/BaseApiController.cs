using CSGProHackathonAPI.Infrastructure;
using CSGProHackathonAPI.Shared.Infrastructure;
using CSGProHackathonAPI.Shared.Models;
using CSGProHackathonAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CSGProHackathonAPI.ApiControllers
{
    public class BaseApiController<TModelType> : ApiController where TModelType : BaseModel
    {
        protected User GetCurrentUser()
        {
            User user = null;

            if (User.Identity.IsAuthenticated)
            {
                user = ((UserIdentity)User.Identity).User;
            }

            return user;
        }

        protected void ValidateViewModel(BaseViewModel<TModelType> viewModel, User user)
        {
            // If we have validation messages...
            // then add each of the messages to the model state.
            var validationMessages = viewModel.GetValidationMessages(user);
            if (validationMessages.Count > 0)
            {
                foreach (var validationMessage in validationMessages)
                {
                    ModelState.AddModelError(validationMessage.Key, validationMessage.Message);
                }
            }
        }

        protected ForbiddenActionResult Forbidden(string message)
        {
            return new ForbiddenActionResult(Request, message);
        }

        protected NoContentActionResult NoContent()
        {
            return new NoContentActionResult(Request);
        }
    }
}