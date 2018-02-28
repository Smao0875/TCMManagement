using System.Collections.Generic;
using System.Linq;
using TCMManagement.Models;
using System.Data.Entity;
using static TCMManagement.BusinessLayer.Constants;
using TCMManagement.BusinessLayer;
using System;
using System.Web.Http.OData;

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
            // check the start time of the new appointment
            var conflictList = context.Appointments
                                .Where(ea => ea.TimeStart <= a.TimeStart)
                                .Where(ea => ea.TimeEnd >= a.TimeStart)
                                .ToList();
            foreach(var conflictAppointment in conflictList)
            {
                // it means the start time is between an existed appointment 
                if ((conflictAppointment.PersonId == a.PersonId) || (conflictAppointment.PatientId == a.PatientId))
                    return null;
            }
            // check the end time of the new appointment
            conflictList = context.Appointments
                    .Where(ea => ea.TimeStart <= a.TimeEnd)
                    .Where(ea => ea.TimeEnd >= a.TimeEnd)
                    .ToList();
            foreach (var conflictAppointment in conflictList)
            {
                // it means the end time is between an existed appointment 
                if ((conflictAppointment.PersonId == a.PersonId) || (conflictAppointment.PatientId == a.PatientId))
                    return null;
            }

            // no conflict, add the appointment
            context.Appointments.Add(a);
            SaveChanges();
            return context.Appointments.ToList().Last();
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
                {
                    List<KeyValuePair<string, string>> queryList = queryParams.ToList();
                    DateTime timeStart = DateTime.Parse(queryList[1].Value);
                    DateTime timeEnd = DateTime.Parse(queryList[2].Value);
                    return context.Appointments
                                            .Where(a => a.PersonId == id)
                                            .Where(a => a.TimeStart >= timeStart)
                                            .Where(a => a.TimeEnd <= timeEnd).ToList();
                }
            }
            return (new List<Appointment>());
        }
       

        public Appointment GetItemById(int id)
        {
            return context.Appointments.FirstOrDefault(a => a.AppointmentId == id);
        }

        // no need for appointment
        public Appointment SearchItem(string s)
        {
            return null;
        }

        // using Delta, but need namespace System.Web.Http.OData
        public bool UpdateItem(int id, Delta<Appointment> a)
        {
            // We need to double check email duplication here.
            Appointment appointmentToUpdate = GetItemById(id);

            if (appointmentToUpdate == null)
            {
                return false;
            }

            a.Patch(appointmentToUpdate);
            SaveChanges();
            return true;
        }

        public bool UpdateItem(int id, Appointment p)
        {
            return true;
        }

        public bool DeleteItem(int id)
        {
            Appointment a = GetItemById(id);
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