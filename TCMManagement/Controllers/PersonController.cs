using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using TCMManagement.BusinessLayer;
using TCMManagement.Models;
using static TCMManagement.BusinessLayer.Constants;
using System.Net;
using AutoMapper;

namespace TCMManagement.Controllers
{
    /// <summary>
    /// All Controllers should follow CRUD sequence so it will be easier to maintain the code.
    /// </summary>
    public class PersonController : ApiController
    {
        private readonly IEntityServices<Person> personService;
        private readonly IMapper mapper;

        public PersonController(IEntityServices<Person> service, IMapper m)
        {
            personService = service;
            mapper = m;
        }

        // Comment our for now, easier to test
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public IHttpActionResult AddPerson(PersonCreation p)
        {
            Person person = personService.CreateItem(mapper.Map<Person>(p));
            if(person == null)
                return Conflict(); // "This email is already taken by others."

            return Ok(personService.GetItems(null).Last());
        }

        // querystring = "?type=practitioner"
        [HttpGet]
        public IEnumerable<Person> GetAllPersons()
        {
            var keyValuePairs = ControllerContext.Request.GetQueryNameValuePairs();
            return personService.GetItems(keyValuePairs);
        }

        [HttpGet]
        [Route("api/person/{id}/{include}")]
        public IHttpActionResult GetPerson(int id, int include = (int)Include.None)
        {
            var person = personService.GetItemById(id, (Include)include);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        // Comment our for now, easier to test
        // [Authorize(Roles = "admin")]
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
