using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.Results;
using TCMManagement.Controllers;
using TCMManagement.Models;
using Moq;
using AutoMapper;
using System;
using TCMManagement.ErrorHelper;
using System.Web.Http;
using System.Net.Http;
using System.Collections.Generic;
using System.Web.Http.OData;

namespace TCMManagement.Test
{
    [TestClass]
    public class TestPersonController
    {
        private Mock<IMapper> mocker;

        [TestInitialize]
        public void Setup()
        {
            mocker = new Mock<IMapper>();
            mocker.Setup(x => x.Map<Person>(It.IsAny<PersonCreation>()))
                .Returns((PersonCreation pc) =>
                {
                    return new Person
                    {
                        FirstName = pc.FirstName,
                        LastName = pc.LastName,
                        Email = pc.Email,
                        Gender = pc.Gender,
                        DateCreated = pc.DateCreated,
                        Phone = pc.Phone,
                        Note = pc.Note,
                        Password = pc.Password,
                        UserRoleId = pc.UserRoleId
                    };
                });
        }

        [TestMethod]
        public void AddPerson_ShouldCreatePerson()
        {
            var context = new TestPersonService();
            var controller = new PersonController(context, mocker.Object);

            var result = controller.AddPerson(GetPartialPerson()) as OkNegotiatedContentResult<Person>;

            Assert.IsNotNull(result);
            Assert.AreEqual("Admin", result.Content.FirstName);
        }

        [TestMethod]
        public void AddPerson_ShouldReturnConflict()
        {
            var context = new TestPersonService();
            var controller = new PersonController(context, mocker.Object);
            controller.AddPerson(GetPartialPerson());

            IHttpActionResult result = controller.AddPerson(GetPartialPerson());

            Assert.IsInstanceOfType(result, typeof(ConflictResult));
        }

        [TestMethod]
        public void GetAllPersons_ShouldReturnAllPersons()
        {
            var context = new TestPersonService();
            context.CreateItem(new Person() { PersonId = 1, UserRoleId = 4, FirstName = "Bob", LastName = "Bob", Email = "1" });
            context.CreateItem(new Person() { PersonId = 2, UserRoleId = 4, FirstName = "Admin", LastName = "2", Email = "2" });

            var controller = new PersonController(context, mocker.Object);
            var result = controller.GetAllPersons() as List<Person>;
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetPerson_ShouldReturnPersonWithSameID()
        {
            var context = new TestPersonService();
            context.CreateItem(GetDemoPerson());

            var controller = new PersonController(context, mocker.Object);
            var result = controller.GetPerson(7) as OkNegotiatedContentResult<Person>;

            Assert.IsNotNull(result);
            Assert.AreEqual(7, result.Content.PersonId);
        }

        [TestMethod]
        public void GetPerson_ShouldReturnNotFoundException()
        {
            var context = new TestPersonService();
            context.CreateItem(GetDemoPerson());
            var controller = new PersonController(context, mocker.Object);

            try
            {
                var result = controller.GetPerson(5) as OkNegotiatedContentResult<Person>;
            }
            catch(ApiDataException e)
            {
                Assert.AreEqual("No product found for this id.", e.ErrorDescription);
                Assert.AreEqual(1001, e.ErrorCode);
            }
        }

        [TestMethod]
        public void PatchPerson_ShouldChangePropertyOfAPerson()
        {
            var context = new TestPersonService();
            context.CreateItem(GetDemoPerson());
            var controller = new PersonController(context, mocker.Object);
            var delta = new Delta<Person>(typeof(Person));
            delta.TrySetPropertyValue("FirstName", "patch");

            var result = controller.PatchPerson(7, delta) as OkNegotiatedContentResult<int>;
            Assert.AreEqual(7, result.Content);
        }

        [TestMethod]
        public void DeletePerson_ShouldReturnOK()
        {
            var context = new TestPersonService();
            context.CreateItem(GetDemoPerson());
            var controller = new PersonController(context, mocker.Object);

            var result = controller.DeletePerson(7);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void DeletePerson_ShouldReturnNotFound()
        {
            var context = new TestPersonService();
            context.CreateItem(GetDemoPerson());
            var controller = new PersonController(context, mocker.Object);

            var result = controller.DeletePerson(1);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private Person GetDemoPerson() => new Person() { PersonId = 7, UserRoleId = 4, FirstName = "Admin", LastName = "2", Email = "email" };

        private PersonCreation GetPartialPerson() => new PersonCreation() { UserRoleId = 4, FirstName = "Admin", LastName = "2", Email = "email" };
    }
}
