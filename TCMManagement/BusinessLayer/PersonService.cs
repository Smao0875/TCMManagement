using System.Collections.Generic;
using System.Linq;
using TCMManagement.Models;
using System.Data.Entity;
using static TCMManagement.BusinessLayer.Constants;
using TCMManagement.BusinessLayer;

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
            if(SearchItem(p.Email) != null)
                return null;

            context.People.Add(p);
            SaveChanges();
            return context.People.Last();
        }

        public IEnumerable<Person> GetItems(IEnumerable<KeyValuePair<string, string>> queryParams = null)
        {
            if(!Utils.IsNullOrEmpty(queryParams)){
                return context.People
                            .Include(p => p.Role)
                            .Where(e => e.Role.Description == queryParams.First().Value)
                            .ToList();
            }
            return context.People.Include(p => p.Role).ToList();
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

        public Person SearchItem(string s, Include include = Include.None)
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
                // https://stackoverflow.com/questions/10324615/partial-update-with-asp-net-web-api
                // use automapper later.
                if(p.FirstName != null) person.LastName = p.LastName;
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
            SaveChanges();
            return true;
        }

        public void MarkAsModified(Person item)
        {
            context.Entry(item).State = EntityState.Modified;
        }

        public int SaveChanges()
        {
            Utils.SoftDeleteEntry(context);
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}