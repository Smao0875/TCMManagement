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
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetItems(IEnumerable<KeyValuePair<string, string>> queryParams = null)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        
        Person IEntityServices<Person>.SearchItem(string s)
        {
            throw new NotImplementedException();
        }
    }
}
