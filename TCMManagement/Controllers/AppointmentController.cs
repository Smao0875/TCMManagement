using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using TCMManagement.BusinessLayer;
using TCMManagement.Models;
using static TCMManagement.BusinessLayer.Constants;

namespace TCMManagement.Controllers
{
    /// <summary>
    /// All Controllers should follow CRUD sequence so it will be easier to maintain the code.
    /// </summary>
    public class AppointmentController : ApiController
    {
        private IEntityServices<Appointment> appointmentService;

        public AppointmentController() {
            appointmentService = new AppointmentService();
        }

        public AppointmentController(IEntityServices<Appointment> service)
        {
            appointmentService = service;
        }

        // Comment our for now, easier to test
        // [Authorize(Roles = "Receptionist")]
        [HttpPost]
        public IHttpActionResult AddAppointment(Appointment a)
        {
            return Ok(appointmentService.CreateItem(a));
        }

        // querystring = "?person=1"
        [HttpGet]
        public IEnumerable<Appointment> GetPersonAppointments(int personId, DateTime timeStart, DateTime timeEnd)
        {
            return appointmentService.GetItems(personId, timeStart, timeEnd);
        }

        [HttpGet]
        public IEnumerable<Appointment> GetPatientAppointment(int patientId)
        {
            return appointmentService.GetItems(patientId);
        }

        // Comment our for now, easier to test
        // [Authorize(Roles = "Receptionist")]
        [HttpPut]
        public IHttpActionResult EditAppointment(int id, Appointment a)
        {
            if (appointmentService.UpdateItem(id, a))
            {
                return Ok(id);
            }
            else
            {
                return NotFound();
            }
        }   

        // [Authorize(Roles = "Receptionist")]
        [HttpDelete]
        public IHttpActionResult DeleteAppointment(int id)
        {
            if(appointmentService.DeleteItem(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
