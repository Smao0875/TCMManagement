using System.Collections.Generic;
using System.Linq;
using TCMManagement.Models;
using System.Data.Entity;

namespace TCMManagement.BusinessLayer
{
    public class PersonService : IEntityServices<Person>
    {
        private readonly TcmEntities dal;

        public PersonService()
        {
            dal = new TcmEntities();
        }

        public int CreateItem(Person p)
        {
            dal.People.Add(p);
            dal.SaveChanges();
            return dal.People.Last().PersonId;
        }

        public IEnumerable<Person> GetAllItems()
        {
            return dal.People.ToList();
        }

        public Person GetItemById(int id)
        {
            return dal.People
                      .FirstOrDefault(p => p.PersonId == id);
        }

        public Person SearchItemByName(string s)
        {
            return dal.People
                      .Include(e => e.Role)
                      .FirstOrDefault(p => p.Email == s);
        }

        public bool UpdateItem(int id, Person p)
        {
            var person = dal.People.FirstOrDefault((x) => x.PersonId == 1);
            if (person == null)
            {
                return false;
            }
            else
            {
                person.LastName = p.LastName;
                person.FirstName = p.FirstName;
                person.Email = p.Email;
                person.Gender = p.Gender;
                dal.SaveChanges();
                return true;
            }
        }

        public bool DeleteItem(int id)
        {
            Person p = dal.People.Find(id);
            if (p == null)
            {
                return false;
            }
            dal.People.Remove(p);
            return true;
        }
    }
}