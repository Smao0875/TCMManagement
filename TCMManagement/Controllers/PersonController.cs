using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TCMManagement.BusinessLayer;
using TCMManagement.DataAccessLayer;
using TCMManagement.Models;

namespace TCMManagement.Controllers
{
    /// <summary>
    /// All Controllers should follow CRUD sequence so it will be easier to maintain the code.
    /// </summary>
    public class PersonController : ApiController
    {
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IHttpActionResult AddPerson(Person p)
        {
            PersonBusinessLayer bl = new PersonBusinessLayer();
            return Ok(bl.AddPerson(p));
        }

        public IEnumerable<Person> GetAllPersons()
        {
            PersonBusinessLayer bl = new PersonBusinessLayer();
            return bl.GetPeople();
        }

        [HttpGet]
        public IHttpActionResult GetPerson(int id)
        {
            PersonBusinessLayer bl = new PersonBusinessLayer();
            var person = bl.GetPersonById(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public IHttpActionResult EditPerson(Person p)
        {
            PersonBusinessLayer bl = new PersonBusinessLayer();
            int id = bl.UpdatePerson(p);
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(id);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public IHttpActionResult DeletePerson(int id)
        {
            PersonBusinessLayer bl = new PersonBusinessLayer();
            if(bl.DeletePerson(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
