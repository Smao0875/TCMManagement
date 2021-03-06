using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using TCMManagement.BusinessLayer;
using TCMManagement.Models;

namespace TCMManagement.Controllers
{
    /// <summary>
    /// All Controllers should follow CRUD sequence so it will be easier to maintain the code.
    /// </summary>
    public class AppointmentController : ApiController
    {
        private IEntityServices<Appointment> appointmentService;

        public AppointmentController()
        {
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
            a.DateCreated = DateTime.Now;  
            Appointment createdAppointment = appointmentService.CreateItem(a);
            if (createdAppointment == null)
                return Conflict();       // "This time is conflict with existed appointment."
            return Ok(createdAppointment);
        }

        // querystring = "?person=1"
        [HttpGet]
        public IEnumerable<Appointment> GetAppointments()
        {
            var keyValuePairs = ControllerContext.Request.GetQueryNameValuePairs();
            return appointmentService.GetItems(keyValuePairs);
        }

        [HttpGet]
        public IHttpActionResult GetAppointment(int id)
        {
            var appointment = appointmentService.GetItemById(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }

        // Comment our for now, easier to test
        // [Authorize(Roles = "Receptionist")]
        [HttpPut]
        public IHttpActionResult EditAppointment(int id, Delta<Appointment> a)
        {
            return UpdateAppointment(id, a);
        }

        // Comment our for now, easier to test
        // [Authorize(Roles = "Receptionist")]
        [AcceptVerbs("PATCH")]
        public IHttpActionResult PatchAppointment(int id, Delta<Appointment> a)
        {
            return UpdateAppointment(id, a);
        }

        private IHttpActionResult UpdateAppointment(int id, Delta<Appointment> a)
        {
            // We need to double check email duplication here.
            Appointment appointmentToUpdate = appointmentService.GetItemById(id);
            if (appointmentToUpdate == null)
            {
                return NotFound();
            }

            a.Patch(appointmentToUpdate);
            appointmentService.SaveChanges();
            return Ok(id);
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
