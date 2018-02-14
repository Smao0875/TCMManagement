using System.Collections.Generic;
using System.Linq;
using TCMManagement.Models;
using System.Data.Entity;
using static TCMManagement.BusinessLayer.Constants;
using TCMManagement.BusinessLayer;
using System;

namespace TCMManagement.BusinessLayer
{
    public class AppointmentService : IEntityServices<Appointment>
    {
        private TcmContext context;

        public AppointmentService()
        {
            context = new TcmContext();
        }

        // NOTE: We might need to check the validity of this appointment.
        public Appointment CreateItem(Appointment a)
        {
            context.Appointments.Add(a);
            SaveChanges();
            return context.Appointments.Last();
        }

        public IEnumerable<Appointment> GetItems(IEnumerable<KeyValuePair<string, string>> queryParams = null)
        {
            if(!Utils.IsNullOrEmpty(queryParams)){
                KeyValuePair<string, string> p = queryParams.FirstOrDefault();
                bool isPatient = p.Key == "Patient";
                int id = Int32.Parse(p.Value);

                if(isPatient)
                    return context.Appointments.Where(a => a.PatientId == id ).ToList();
                else
                    return context.Appointments.Where(a => a.PersonId == id ).ToList(); 
            }
            return context.Appointments.ToList();
        }

        public Appointment GetItemById(int id, Include include)
        {
            return context.Appointments.FirstOrDefault(a => a.AppointmentId == id);
        }

        public Appointment SearchItem(string s, Include include)
        {
            return null;
        }

        public bool UpdateItem(int id, Appointment a)
        {
            return true;
        }

        public bool DeleteItem(int id)
        {
            Appointment a = context.Appointments.Find(id);
            if (a == null)
            {
                return false;
            }
            context.Appointments.Remove(a);
            SaveChanges();
            return true;
        }

        public void MarkAsModified(Appointment item)
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