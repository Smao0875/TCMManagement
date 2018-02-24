using System.Collections.Generic;
using System.Linq;
using TCMManagement.Models;
using System.Data.Entity;
using static TCMManagement.BusinessLayer.Constants;
using TCMManagement.BusinessLayer;
using System;

namespace TCMManagement.BusinessLayer
{
    public class MedicalRecordService : IEntityServices<MedicalHistoryRecord>
    {
        private TcmContext context;

        public MedicalRecordService()
        {
            context = new TcmContext();
        }

        // NOTE: We might need to check the validity of this appointment.
        public MedicalHistoryRecord CreateItem(MedicalHistoryRecord m)
        {
            context.MedicalHistoryRecords.Add(m);
            SaveChanges();
            return context.MedicalHistoryRecords.Last();
        }

        public IEnumerable<MedicalHistoryRecord> GetItems(IEnumerable<KeyValuePair<string, string>> queryParams = null)
        {
            if(!Utils.IsNullOrEmpty(queryParams)){
                KeyValuePair<string, string> p = queryParams.FirstOrDefault();
                bool isPatient = p.Key == "Patient";
                int id = Int32.Parse(p.Value);

                return context.MedicalHistoryRecords.Where(a => a.PatientId == id ).ToList();
            }
            return new List<MedicalHistoryRecord>();
        }

        public MedicalHistoryRecord GetItemById(int id)
        {
            return context.MedicalHistoryRecords.FirstOrDefault(a => a.MedicalHistoryRecordId == id);
        }

        public MedicalHistoryRecord SearchItem(string s)
        {
            return null;
        }

        public bool UpdateItem(int id, MedicalHistoryRecord a)
        {
            return true;
        }

        public bool DeleteItem(int id)
        {
            MedicalHistoryRecord a = context.MedicalHistoryRecords.Find(id);
            if (a == null)
            {
                return false;
            }
            context.MedicalHistoryRecords.Remove(a);
            SaveChanges();
            return true;
        }

        public void MarkAsModified(MedicalHistoryRecord item)
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