using System.Collections.Generic;
using System.Linq;
using TCMManagement.Models;
using System.Data.Entity;
using static TCMManagement.BusinessLayer.Constants;

namespace TCMManagement.BusinessLayer
{
    public class PersonService : IEntityServices<Person>
    {
        private TcmContext context;

        public PersonService()
        {
            context = new TcmContext();
        }

        public Person CreateItem(Person p)
        {
            context.People.Add(p);
            context.SaveChanges();
            return context.People.Last();
        }

        public IEnumerable<Person> GetAllItems()
        {
            return context.People.ToList();
        }

        public Person GetItemById(int id, Include include)
        {
            switch (include)
            {
                case Include.None:
                    return context.People.FirstOrDefault(p => p.PersonId == id);
                case Include.Appointment:
                    return context.People
                                  .Include(e => e.Appointments)
                                  .FirstOrDefault(p => p.PersonId == id);
                case Include.MedicalRecord:
                    return context.People
                                  .Include(e => e.TreatmentRecords)
                                  .FirstOrDefault(p => p.PersonId == id);
                case Include.Role:
                    return context.People
                                  .Include(e => e.Role)
                                  .FirstOrDefault(p => p.PersonId == id);
                default:
                    return null;
            }
        }

        public Person SearchItem(string s, Include include)
        {
            return context.People
                      .Include(e => e.Role)
                      .FirstOrDefault(p => p.Email == s);
        }

        public bool UpdateItem(int id, Person p)
        {
            var person = context.People.FirstOrDefault((x) => x.PersonId == 1);
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
                SaveChanges();
                return true;
            }
        }

        public bool DeleteItem(int id)
        {
            Person p = context.People.Find(id);
            if (p == null)
            {
                return false;
            }
            context.People.Remove(p);
            return true;
        }

        public void MarkAsModified(Person item)
        {
            context.Entry(item).State = EntityState.Modified;
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}