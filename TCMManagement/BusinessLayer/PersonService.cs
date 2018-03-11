﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TCMManagement.Models;

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
            SaveChanges();
            return context.People.ToList().Last();
        }

        public IEnumerable<Person> GetItems(IEnumerable<KeyValuePair<string, string>> queryParams = null)
        {
            if(!Utils.IsNullOrEmpty(queryParams)){
                string roleDescription = queryParams.First().Value;
                return context.People
                            .Include(p => p.Role)
                            .Where(e => e.Role.Description == roleDescription)
                            .ToList();
            }
            return context.People.Include(p => p.Role).ToList();
        }

        public Person GetItemById(int id)
        {
            return context.People
                            .Include(e => e.Role)
                            .Include(e => e.Appointments)
                            .Include(e => e.TreatmentRecords)
                            .FirstOrDefault(p => p.PersonId == id);
        }

        public Person SearchItem(string s)
        {
            return context.People
                          .Include(e => e.Role)
                          .Include(e => e.Appointments)
                          .Include(e => e.TreatmentRecords)
                          .FirstOrDefault(p => p.Email == s);
        }

        public bool UpdateItem(int id, Person p)
        {
            return true;
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