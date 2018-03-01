using System.Linq;
using TCMManagement.Models;

namespace TCMManagement.Test
{
    class TestPersonDbSet : TestDbSet<Person>
    {
        public override Person Find(params object[] keyValues)
        {
            if(keyValues.Single() is int)
                return this.SingleOrDefault(person => person.PersonId == (int)keyValues.Single());

            if(keyValues.Single() is string)
                return this.SingleOrDefault(person => person.Email == (string)keyValues.Single());

            return null;
        }
    }
}
