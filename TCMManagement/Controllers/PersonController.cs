using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
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
            p.DateCreated = DateTime.Now;// used for debug.
            Person person = personService.CreateItem(mapper.Map<Person>(p));
            if(person == null)
                return Conflict(); // "This email is already taken by others."

            return Ok(person);
        }

        // querystring = "?type=practitioner"
        [HttpGet]
        public IEnumerable<Person> GetAllPersons()
        {
            var keyValuePairs = ControllerContext.Request.GetQueryNameValuePairs();
            return personService.GetItems(keyValuePairs);
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

        // Comment our for now, easier to test
        // [Authorize(Roles = "admin")]
        [HttpPut]
        public IHttpActionResult EditPerson(int id, Delta<Person> p)
        {
            return UpdatePerson(id, p);
        }

        [AcceptVerbs("PATCH")]
        public IHttpActionResult PatchPerson(int id, Delta<Person> p)
        {
            return UpdatePerson(id, p);
        }

        // This method might need to be moved to business layer.
        private IHttpActionResult UpdatePerson(int id, Delta<Person> p)
        {
            // We need to double check email duplication here.
            Person person = personService.GetItemById(id);
            if(person == null)
            {
                return NotFound();
            }

            p.Patch(person);
            personService.SaveChanges();
            return Ok(id);
        }

        //[Authorize(Roles = "admin")]
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
