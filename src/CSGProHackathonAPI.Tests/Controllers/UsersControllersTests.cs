using CSGProHackathonAPI.ApiControllers;
using CSGProHackathonAPI.Infrastructure;
using CSGProHackathonAPI.Messages;
using CSGProHackathonAPI.Shared.Data;
using CSGProHackathonAPI.Shared.Models;
using CSGProHackathonAPI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Routing;

namespace CSGProHackathonAPI.Tests.Controllers
{
    [TestClass]
    public class UsersControllersTests
    {
        [TestMethod]
        public void UsersController_Post_NoViewModelReturnsBadRequest()
        {
            // Arrange
            var usersController = new UsersController(null);

            // Act
            var actionResult = usersController.Post(null);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InvalidModelStateResult));
        }

        [TestMethod]
        public void UsersController_Post_InvalidModelStateReturnsErrorRequest()
        {
            // Arrange
            var usersController = new UsersController(null);
            var viewModel = new UserAddViewModel();

            // Add a model state error.
            usersController.ModelState.AddModelError("UserName", "UserName is required.");

            // Act
            var actionResult = usersController.Post(viewModel);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ErrorActionResult));
        }

        [TestMethod]
        public void UsersController_Post_ValidModelStateSavesUser()
        {
            // Arrange

            var repository = Substitute.For<IRepository>();

            var usersController = new UsersController(repository);
            usersController.Request = new HttpRequestMessage();
            usersController.Configuration = new HttpConfiguration();

            // Mock the UrlHelper in order for the controller 
            // to be able to successfully call the Url.Link() method.
            var urlHelper = Substitute.For<UrlHelper>();
            urlHelper.Link(Arg.Any<string>(), Arg.Any<object>()).Returns("http://localhost/api/users/0");
            usersController.Url = urlHelper;

            var viewModel = new UserAddViewModel()
            {
                UserName = "johns",
                Password = "password"
            };

            // Act
            var actionResult = usersController.Post(viewModel) as CreatedNegotiatedContentResult<UserMessage>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(0, actionResult.Content.UserId);
            Assert.AreEqual("johns", actionResult.Content.UserName);
            repository.ReceivedWithAnyArgs().SaveUser(null);
        }
    }
}
