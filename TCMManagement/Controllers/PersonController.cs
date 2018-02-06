using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TCMManagement.DataAccessLayer;
using TCMManagement.Models;

namespace TCMManagement.Controllers
{
    /// <summary>
    /// All Controllers should follow CRUD sequence so it will be easier to maintain the code.
    /// </summary>
    public class PersonController : ApiController
    {
        [HttpPost]
        public IHttpActionResult AddPerson(Person p)
        {
            TcmDAL dal = new TcmDAL();
            dal.People.Add(p);
            dal.SaveChanges();
            return Ok();
        }

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
            var person = dal.People.FirstOrDefault((p) => p.PersonId == 1);
            if (person == null)
            {
                return NotFound();
            }
            else
            {
                person.LastName = per.LastName;
                person.FirstName = per.FirstName;
                person.Email = per.Email;
                person.Gender = per.Gender;
                dal.SaveChanges();
                return Ok();
            }
        }
    }
}
