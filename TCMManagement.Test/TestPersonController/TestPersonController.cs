using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.Results;
using TCMManagement.Controllers;
using TCMManagement.Models;

namespace TCMManagement.Test
{
    [TestClass]
    public class TestPersonController
    {
        //[TestMethod]
        //public void GetPerson_ShouldReturnPersonWithSameID()
        //{
        //    var context = new TestPersonService();
        //    context.CreateItem(GetDemoPerson());

        //    var controller = new PersonController(context);
        //    var result = controller.GetPerson(7) as OkNegotiatedContentResult<Person>;

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(7, result.Content.PersonId);
        //}

        //private Person GetDemoPerson()
        //{
        //    return new Person() { PersonId = 7, UserRoleId = 4, FirstName = "Admin", LastName = "2", Email = "email" };
        //}
    }
}
