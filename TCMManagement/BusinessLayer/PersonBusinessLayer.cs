using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TCMManagement.DataAccessLayer;
using TCMManagement.Models;

namespace TCMManagement.BusinessLayer
{
    public class PersonBusinessLayer
    {
        public int AddPerson(Person p)
        {
            TcmDAL dal = new TcmDAL();
            dal.People.Add(p);
            dal.SaveChanges();
            return dal.People.Last().PersonId;
        }

        public List<Person> GetPeople()
        {
            TcmDAL dal = new TcmDAL();
            return dal.People.ToList();
        }

        public Person GetPersonById(int id)
        {
            TcmDAL dal = new TcmDAL();
            return dal.People.FirstOrDefault((p) => p.PersonId == id);
        }

        public int UpdatePerson(Person p)
        {
            TcmDAL dal = new TcmDAL();
            var person = dal.People.FirstOrDefault((x) => x.PersonId == 1);
            if (person == null)
            {
                return 0;
            }
            else
            {
                person.LastName = p.LastName;
                person.FirstName = p.FirstName;
                person.Email = p.Email;
                person.Gender = p.Gender;
                dal.SaveChanges();
                return p.PersonId;
            }
        }

        public bool DeletePerson(int id)
        {
            TcmDAL dal = new TcmDAL();
            var person = dal.People.FirstOrDefault((p) => p.PersonId == id);
            if (person == null)
            {
                return false;
            }
            dal.People.Remove(person);
            dal.SaveChanges();
            return true;
        }
    }
}