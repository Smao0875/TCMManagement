using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TCMManagement.BusinessLayer;
using TCMManagement.Models;
using Moq;
using System.Data.Entity;
using System.Linq;

using System.Threading.Tasks;


namespace TCMManagement.Test
{
    [TestClass]
    public class TestAppointmentService 
    {
        [TestMethod]
        public void TestCreateItem()
        {
            // initialize the database with an appointment
            var appointmentList = new List<Appointment>
            {
                new Appointment() { AppointmentId = 0, PersonId = 1, PatientId = 1, Description = "initilization 0", DateCreated = DateTime.Now,
                                    TimeStart = DateTime.Parse("2018-03-02 09:00:00"), TimeEnd = DateTime.Parse("2018-03-02 10:00:00")},
                new Appointment() { AppointmentId = 1, PersonId = 2, PatientId = 2, Description = "initilization 1", DateCreated = DateTime.Now,
                                    TimeStart = DateTime.Parse("2018-03-02 15:00:00"), TimeEnd = DateTime.Parse("2018-03-02 16:00:00")}
            };
            var appointmentQuery = appointmentList.AsQueryable();
            // the appointment list which has no conflict with the appointments in database
            var appointmentNoConflictList = new List<Appointment>
            {
                new Appointment() { AppointmentId = 2, PersonId = 1, PatientId = 2, Description = "No Conflict 0", DateCreated = DateTime.Now,
                                    TimeStart = DateTime.Parse("2018-03-02 10:00:00"), TimeEnd = DateTime.Parse("2018-03-02 11:00:00")}
            };
            // the appointment list which has conflict with the appointments in database
            var appointmentConflictList = new List<Appointment>
            {
                new Appointment() { AppointmentId = 3, PersonId = 1, PatientId = 2, Description = "Conflict with 0 in start", DateCreated = DateTime.Now,
                                    TimeStart = DateTime.Parse("2018-03-02 9:30:00"), TimeEnd = DateTime.Parse("2018-03-02 10:00:00")},
                new Appointment() { AppointmentId = 4, PersonId = 2, PatientId = 1, Description = "Conflict with 2 in end", DateCreated = DateTime.Now,
                                    TimeStart = DateTime.Parse("2018-03-02 14:00:00"), TimeEnd = DateTime.Parse("2018-03-02 15:30:00")},
            };

            var mockSet = new Mock<DbSet<Appointment>>();
            mockSet.As<IQueryable<Appointment>>().Setup(m => m.Provider).Returns(appointmentQuery.Provider);
            mockSet.As<IQueryable<Appointment>>().Setup(m => m.Expression).Returns(appointmentQuery.Expression);
            mockSet.As<IQueryable<Appointment>>().Setup(m => m.ElementType).Returns(appointmentQuery.ElementType);
            mockSet.As<IQueryable<Appointment>>().Setup(m => m.GetEnumerator()).Returns(value : appointmentQuery.GetEnumerator());

            var mockContext = new Mock<TcmContext>();
            mockContext.Setup(c => c.Appointments).Returns(mockSet.Object);
            var appointmentService = new AppointmentService(mockContext.Object);

            // no conflict even appointmentNoConflictList[0].TimeStart == appointmentList[0].TimeEnd
            var addedAppointment = appointmentService.CreateItem(appointmentNoConflictList[0]);
            Assert.AreEqual<int>(addedAppointment.AppointmentId, 1);
        
            // conflict with appointment 0 in TimeStart
            addedAppointment = appointmentService.CreateItem(appointmentConflictList[0]);
            Assert.AreEqual<Appointment>(null, null);
            // conflict with appointment 2 in TimeEnd
            addedAppointment = appointmentService.CreateItem(appointmentConflictList[1]);
            Assert.AreEqual<Appointment>(null, null);
        }

        [TestMethod]
        public void TestGetItemById()
        {
            var appointmentList = new List<Appointment>
            {
                 new Appointment()
                 {
                    AppointmentId = 0,
                    PersonId = 1,
                    PatientId = 1,
                    Description = "hurt",
                    DateCreated = DateTime.Now,
                    TimeStart = DateTime.Parse("2018-03-02 09:00:00"),
                    TimeEnd = DateTime.Parse("2018-03-02 10:00:00")
                 }
            };
            var appointmentQuery = appointmentList.AsQueryable();

            var mockSet = new Mock<DbSet<Appointment>>();
            mockSet.As<IQueryable<Appointment>>().Setup(m => m.Provider).Returns(appointmentQuery.Provider);
            mockSet.As<IQueryable<Appointment>>().Setup(m => m.Expression).Returns(appointmentQuery.Expression);
            mockSet.As<IQueryable<Appointment>>().Setup(m => m.ElementType).Returns(appointmentQuery.ElementType);
            mockSet.As<IQueryable<Appointment>>().Setup(m => m.GetEnumerator()).Returns(value: appointmentQuery.GetEnumerator());
           
            var mockContext = new Mock<TcmContext>();
            mockContext.Setup(c => c.Appointments).Returns(mockSet.Object);
            
            var appointmentService = new AppointmentService(mockContext.Object);
            var getAppointment = appointmentService.GetItemById(appointmentList[0].AppointmentId);
            Assert.AreEqual<int>(getAppointment.AppointmentId, appointmentList[0].AppointmentId);
        }

        public void TestGetItems()
        {

        }
    }
}
