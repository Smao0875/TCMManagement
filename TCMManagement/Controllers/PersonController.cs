using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TCMManagement.BusinessLayer;
using TCMManagement.Models;

namespace TCMManagement.Controllers
{
    /// <summary>
    /// All Controllers should follow CRUD sequence so it will be easier to maintain the code.
    /// </summary>
    public class PersonController : ApiController
    {
        private readonly IEntityServices<Person> personService;

        public PersonController()
        {
            personService = new PersonService();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IHttpActionResult AddPerson(Person p)
        {
            return Ok(personService.CreateItem(p));
        }

        public IEnumerable<Person> GetAllPersons()
        {
            return personService.GetAllItems();
        }

        [HttpGet]
        public IHttpActionResult GetPerson(int id)
        {
            var person = personService.GetItemById(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public IHttpActionResult EditPerson(int id, Person p)
        {
            personService.UpdateItem(id, p);
            return Ok(id);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public IHttpActionResult DeletePerson(int id)
        {
            if(personService.DeleteItem(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
