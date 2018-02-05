using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TCMManagement.DataAccessLayer;
using TCMManagement.Models;

namespace TCMManagement.Controllers
{
    public class PersonController : ApiController
    {
        public IEnumerable<Person> GetAllPersons()
        {
            TcmDAL dal = new TcmDAL();
            return dal.People.ToList();
        }

        [HttpGet]
        public IHttpActionResult GetPerson(int id)
        {
            TcmDAL dal = new TcmDAL();
            var person = dal.People.FirstOrDefault((p) => p.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPut]
        public IHttpActionResult EditPerson(Person per)
        {
            TcmDAL dal = new TcmDAL();
            var person = dal.People.FirstOrDefault((p) => p.PersonId == p.PersonId);
            if (person == null)
            {
                return NotFound();
            }
            else
            {
                person.LastName = per.LastName;
                person.FirstName = per.FirstName;
                return Ok();
            }
        }
    }
}
