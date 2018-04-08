using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TCMManagement.Models;

namespace TCMManagement.BusinessLayer
{
    public class AppointmentService : IEntityServices<Appointment>
    {
        private TcmContext context;

        public AppointmentService()
        {
            context = new TcmContext();
        }

        public AppointmentService(TcmContext inputContext)
        {
            context = inputContext;
        }

        // NOTE: We might need to check the validity of this appointment.
        public Appointment CreateItem(Appointment a)
        {
            // check the start time of the new appointment
            var conflictList = context.Appointments
                                .Where(ea => ea.PersonId == a.PersonId)
                                .ToList();
            foreach (var conflictAppointment in conflictList)
            {
                // any time overlap will cause conflict
                if ((conflictAppointment.TimeStart >= a.TimeStart && conflictAppointment.TimeStart < a.TimeEnd)
                 || (conflictAppointment.TimeEnd > a.TimeStart && conflictAppointment.TimeEnd <= a.TimeEnd))
                {
                    return null;
                }
            }
            // no conflict, add the appointment
            context.Appointments.Add(a);
            SaveChanges();
            return context.Appointments.Include(ap => ap.Patient).ToList().Last();
        }

        public IEnumerable<Appointment> GetItems(IEnumerable<KeyValuePair<string, string>> queryParams = null)
        {
            if(!Utils.IsNullOrEmpty(queryParams)){
                KeyValuePair<string, string> p = queryParams.FirstOrDefault();
                bool isPatient = p.Key == "Patient";
                int id = Int32.Parse(p.Value);

                if (isPatient)
                {
                    return context.Appointments.Where(a => a.PatientId == id).Include(a => a.Patient).ToList();
                }
                else
                {
                    List<KeyValuePair<string, string>> queryList = queryParams.ToList();
                    if (queryList.Count() == 1)
                    {
                        return context.Appointments.Where(a => a.PersonId == id).Include(a => a.Patient).ToList();
                    }
                    else
                    {
                        DateTime timeStart = DateTime.Parse(queryList[1].Value);
                        DateTime timeEnd = DateTime.Parse(queryList[2].Value);
                        if (timeStart > timeEnd)
                        {
                            // make sure timeEnd >= timeStart
                            DateTime tmp = timeStart;
                            timeStart = timeEnd;
                            timeEnd = timeStart;
                        }
                        return context.Appointments
                                                .Where(a => a.PersonId == id)
                                                .Where(a => a.TimeStart >= timeStart)
                                                .Where(a => a.TimeEnd <= timeEnd)
                                                .Include(a => a.Patient).ToList();
                    }
                }
            }
            return context.Appointments.Include(a => a.Patient).ToList();
        }
       

        public Appointment GetItemById(int id)
        {
            return context.Appointments.Include(a => a.Patient).FirstOrDefault(a => a.AppointmentId == id);
        }

        // no need for appointment
        public Appointment SearchItem(string s)
        {
            return null;
        }

        // using Delta, but need namespace System.Web.Http.OData
        /*
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
        */

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