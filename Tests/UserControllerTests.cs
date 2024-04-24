using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using System.Web.Mvc;

namespace CRUD_application_2.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void Index_ReturnsViewWithUserList()
        {
            // Arrange
            var controller = new UserController();
            var userlist = new List<User>
            {
                new User { Id = 1, Name = "John" },
                new User { Id = 2, Name = "Jane" }
            };
            UserController.userlist = userlist;

            // Act
            var result = controller.Index() as ViewResult;
            var model = result.Model as List<User>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userlist.Count, model.Count);
            Assert.AreEqual(userlist[0].Id, model[0].Id);
            Assert.AreEqual(userlist[0].Name, model[0].Name);
            Assert.AreEqual(userlist[1].Id, model[1].Id);
            Assert.AreEqual(userlist[1].Name, model[1].Name);
        }

        [TestMethod]
        public void Details_ExistingUserId_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            var userlist = new List<User>
            {
                new User { Id = 1, Name = "John" },
                new User { Id = 2, Name = "Jane" }
            };
            UserController.userlist = userlist;
            var userId = 1;

            // Act
            var result = controller.Details(userId) as ViewResult;
            var model = result.Model as User;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userlist[0].Id, model.Id);
            Assert.AreEqual(userlist[0].Name, model.Name);
        }

        [TestMethod]
        public void Details_NonExistingUserId_ReturnsHttpNotFound()
        {
            // Arrange
            var controller = new UserController();
            var userlist = new List<User>
            {
                new User { Id = 1, Name = "John" },
                new User { Id = 2, Name = "Jane" }
            };
            UserController.userlist = userlist;
            var userId = 3;

            // Act
            var result = controller.Details(userId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void Create_ValidUser_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var userlist = new List<User>();
            UserController.userlist = userlist;
            var user = new User { Id = 1, Name = "John" };

            // Act
            var result = controller.Create(user) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Create_InvalidUser_ReturnsViewWithErrors()
        {
            // Arrange
            var controller = new UserController();
            var userlist = new List<User>();
            UserController.userlist = userlist;
            var user = new User { Id = 1, Name = "" };
            controller.ModelState.AddModelError("Name", "The Name field is required.");

            // Act
            var result = controller.Create(user) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(controller.ModelState.IsValid);
        }

        [TestMethod]
        public void Edit_ExistingUserId_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            var userlist = new List<User>
            {
                new User { Id = 1, Name = "John" },
                new User { Id = 2, Name = "Jane" }
            };
            UserController.userlist = userlist;
            var userId = 1;

            // Act
            var result = controller.Edit(userId) as ViewResult;
            var model = result.Model as User;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userlist[0].Id, model.Id);
            Assert.AreEqual(userlist[0].Name, model.Name);
        }

        [TestMethod]
        public void Edit_NonExistingUserId_ReturnsHttpNotFound()
        {
            // Arrange
            var controller = new UserController();
            var userlist = new List<User>
            {
                new User { Id = 1, Name = "John" },
                new User { Id = 2, Name = "Jane" }
            };
            UserController.userlist = userlist;
            var userId = 3;

            // Act
            var result = controller.Edit(userId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void Delete_ExistingUserId_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var userlist = new List<User>
            {
                new User { Id = 1, Name = "John" },
                new User { Id = 2, Name = "Jane" }
            };
            UserController.userlist = userlist;
            var userId = 1;

            // Act
            var result = controller.Delete(userId) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Delete_NonExistingUserId_ReturnsHttpNotFound()
        {
            // Arrange
            var controller = new UserController();
            var userlist = new List<User>
            {
                new User { Id = 1, Name = "John" },
                new User { Id = 2, Name = "Jane" }
            };
            UserController.userlist = userlist;
            var userId = 3;

            // Act
            var result = controller.Delete(userId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }
    }
}
