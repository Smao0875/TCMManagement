using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TCMManagement.Models;

namespace TCMManagement.BusinessLayer
{
    public class PatientService : IEntityServices<Patient>
    {
        private TcmContext context;
        public PatientService()
        {
            context = new TcmContext();
        }

        public PatientService(TcmContext inputContext)
        {
            context = inputContext;
        }

        public Patient CreateItem(Patient p)
        {
            p.UserRoleId = 1;
            context.Patients.Add(p);
            SaveChanges();
            return context.Patients.ToList().Last();
        }

        public IEnumerable<Patient> GetItems(IEnumerable<KeyValuePair<string, string>> queryParams = null)
        {
            if (!Utils.IsNullOrEmpty(queryParams))
            {
                // only support get patient thru email, phone number, and firstname (1st) + lastname (2nd)
                List<KeyValuePair<string, string>> queryList = queryParams.ToList();
                if (queryParams.FirstOrDefault().Key == "Email")
                {
                    return (new List<Patient>{SearchItem(queryParams.FirstOrDefault().Value)});
                }
                if (queryParams.FirstOrDefault().Key == "Phone")
                {
                    // If I put queryParams.FirstOrDefault().Value to Where() directly, I will get exception
                    string phoneNum = queryParams.FirstOrDefault().Value;
                    if (queryParams.FirstOrDefault().Value != null)
                    {
                        return context.Patients
                            .Include(e => e.Appointments)
                            .Include(e => e.TreatmentRecords)
                            .Where(e => e.Phone == phoneNum)
                            .ToList();
                    } 
                }
                if (queryList[0].Key == "FirstName" && queryList[1].Key == "LastName" )
                {
                    string firstName = queryList[0].Value;
                    string lastName = queryList[1].Value;
                    return context.Patients
                            .Include(e => e.Appointments)
                            .Include(e => e.TreatmentRecords)
                            .Where(e => e.FirstName == firstName)
                            .Where(e => e.LastName == lastName)
                            .ToList();
                }
                // the query is not accepable, return an empty list
                return (new List<Patient>());
            }
            return context.Patients.ToList();
        }

        public Patient GetItemById(int id)
        {
            return context.Patients
                            .Include(e => e.Appointments)
                            .Include(e => e.TreatmentRecords)
                            .FirstOrDefault(p => p.PatientId == id);
        }

        public Patient SearchItem(string s)
        {
            return context.Patients
                          .Include(e => e.Role)
                          .Include(e => e.Appointments)
                          .Include(e => e.TreatmentRecords)
                          .FirstOrDefault(p => p.Email == s);
        }

        public bool UpdateItem(int id, Patient p)
        {
            return true;
        }

        public bool DeleteItem(int id)
        {
            Patient p = context.Patients.Find(id);
            if (p == null)
            {
                return false;
            }
            context.Patients.Remove(p);
            SaveChanges();
            return true;
        }

        public void MarkAsModified(Patient item)
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