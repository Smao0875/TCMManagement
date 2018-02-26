using System.Collections.Generic;
using System.Linq;
using TCMManagement.Models;
using System.Data.Entity;
using static TCMManagement.BusinessLayer.Constants;
using TCMManagement.BusinessLayer;
using System;

namespace TCMManagement.BusinessLayer
{
    public class TreatmentRecordService : IEntityServices<TreatmentRecord>
    {
        private TcmContext context;

        public TreatmentRecordService()
        {
            context = new TcmContext();
        }

        // NOTE: We might need to check the validity of this appointment.
        public TreatmentRecord CreateItem(TreatmentRecord t)
        {
            context.TreatmentRecords.Add(t);
            SaveChanges();
            return context.TreatmentRecords.ToList().Last();
        }

        public IEnumerable<TreatmentRecord> GetItems(IEnumerable<KeyValuePair<string, string>> queryParams = null)
        {
            if(!Utils.IsNullOrEmpty(queryParams)){
                KeyValuePair<string, string> p = queryParams.FirstOrDefault();
                bool isPatient = p.Key == "Patient";
                int id = Int32.Parse(p.Value);

                if(isPatient)
                    return context.TreatmentRecords.Where(a => a.PatientId == id ).ToList();
                else
                    return context.TreatmentRecords.Where(a => a.PersonId == id ).ToList(); 
            }
            return context.TreatmentRecords.ToList();
        }

        public TreatmentRecord GetItemById(int id)
        {
            return context.TreatmentRecords.FirstOrDefault(a => a.TreatmentRecordId == id);
        }

        public TreatmentRecord SearchItem(string s)
        {
            return null;
        }

        public bool UpdateItem(int id, TreatmentRecord a)
        {
            return true;
        }

        public bool DeleteItem(int id)
        {
            TreatmentRecord a = context.TreatmentRecords.Find(id);
            if (a == null)
            {
                return false;
            }
            context.TreatmentRecords.Remove(a);
            SaveChanges();
            return true;
        }

        public void MarkAsModified(TreatmentRecord item)
        {
            context.Entry(item).State = EntityState.Modified;
        }

        public int SaveChanges()
        {
#if DEBUG
            context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
#endif
            Utils.SoftDeleteEntry(context);
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}