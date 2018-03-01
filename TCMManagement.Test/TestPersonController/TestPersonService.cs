using System;
using System.Collections.Generic;
using TCMManagement.BusinessLayer;
using TCMManagement.Models;

namespace TCMManagement.Test
{
    class TestPersonService : IEntityServices<Person>
    {
        TestPersonDbSet context = new TestPersonDbSet();

        public Person CreateItem(Person item)
        {
            return context.Add(item);
        }

        public bool DeleteItem(int id)
        {
            if (context.Remove(context.Find(id)) != null)
                return true;

            return false;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetItems(IEnumerable<KeyValuePair<string, string>> queryParams = null)
        {
            Person[] people = new Person[context.Local.Count];
            context.Local.CopyTo(people, 0);

            return new List<Person>(people);
        }

        public Person GetItemById(int id)
        {
            return context.Find(id);
        }

        public void MarkAsModified(Person item)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            return 0;
        }

        public Person SearchItemByName(string s)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(int id, Person item)
        {
            return true;
        }
        
        Person IEntityServices<Person>.SearchItem(string s)
        {
            return context.Find(s);
        }
    }
}
