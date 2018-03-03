using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TCMManagement.BusinessLayer;
using TCMManagement.Models;
using Moq;

namespace TCMManagement.Test
{
    [TestClass]
    public class TestAppointmentService 
    {
        [TestMethod]
        public void TestCreateItem()
        {
            Appointment[] appointmentList = {
                new Appointment() { AppointmentId = 0, PersonId = 1, PatientId = 1, Description = "No Conflict", DateCreated = DateTime.Now,
                                    TimeStart = DateTime.Parse("2018-03-02 09:00:00Z"), TimeEnd = DateTime.Parse("2018-03-02 10:00:00Z")},
                new Appointment() { AppointmentId = 1, PersonId = 1, PatientId = 2, Description = "No Conflict", DateCreated = DateTime.Now,
                                    TimeStart = DateTime.Parse("2018-03-02 10:00:00Z"), TimeEnd = DateTime.Parse("2018-03-02 11:00:00Z")},
                new Appointment() { AppointmentId = 2, PersonId = 2, PatientId = 2, Description = "No Conflict", DateCreated = DateTime.Now,
                                    TimeStart = DateTime.Parse("2018-03-02 15:00:00Z"), TimeEnd = DateTime.Parse("2018-03-02 16:00:00Z")},
                new Appointment() { AppointmentId = 3, PersonId = 1, PatientId = 2, Description = "Conflict with 0 in start", DateCreated = DateTime.Now,
                                    TimeStart = DateTime.Parse("2018-03-02 9:30:00Z"), TimeEnd = DateTime.Parse("2018-03-02 10:00:00Z")},
                new Appointment() { AppointmentId = 4, PersonId = 2, PatientId = 1, Description = "Conflict with 2 in end", DateCreated = DateTime.Now,
                                    TimeStart = DateTime.Parse("2018-03-02 14:00:00Z"), TimeEnd = DateTime.Parse("2018-03-02 15:30:00Z")},
            };


            var appointmentService = new AppointmentService();
            var addedAppointment = appointmentService.CreateItem(appointmentList[0]);
            Assert.AreEqual<int>(addedAppointment.AppointmentId, appointmentList[0].AppointmentId);

            // no conflict even appointmentList[1].TimeStart == appointmentList[0].TimeEnd
            addedAppointment = appointmentService.CreateItem(appointmentList[1]);
            Assert.AreEqual<int>(addedAppointment.AppointmentId, appointmentList[1].AppointmentId);

            appointmentService.CreateItem(appointmentList[2]);
            // conflict with appointment 0 in TimeStart
            addedAppointment = appointmentService.CreateItem(appointmentList[3]);
            Assert.AreEqual<Appointment>(null, null);

            // conflict with appointment 2 in TimeEnd
            addedAppointment = appointmentService.CreateItem(appointmentList[4]);
            Assert.AreEqual<Appointment>(null, null);
        }

        [TestMethod]
        public void TestGetItemById()
        {
            var appointmentService = new AppointmentService();
            var appointment = new Appointment()
            {
                AppointmentId = 0,
                PersonId = 1,
                PatientId = 1,
                Description = "No Conflict",
                DateCreated = DateTime.Now,
                TimeStart = DateTime.Parse("2018-03-02 09:00:00Z"),
                TimeEnd = DateTime.Parse("2018-03-02 10:00:00Z")
            };

            var addedAppointment = appointmentService.CreateItem(appointment);
            var getAppointment = appointmentService.GetItemById(addedAppointment.AppointmentId);
            Assert.AreEqual<int>(getAppointment.AppointmentId, addedAppointment.AppointmentId);
        }

        public void TestGetItems()
        {

        }

        public void TestDeleteItem()
        {

        }
    }
}
