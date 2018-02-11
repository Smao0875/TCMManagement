using System.Linq;
using TCMManagement.Models;

namespace TCMManagement.Test
{
    class TestPersonDbSet : TestDbSet<Person>
    {
        public override Person Find(params object[] keyValues)
        {
            return this.SingleOrDefault(person => person.PersonId == (int)keyValues.Single());
        }

    }
}
