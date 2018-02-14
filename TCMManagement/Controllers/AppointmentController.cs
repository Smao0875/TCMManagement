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
        public IEnumerable<Appointment> GetAppointments()
        {
            var keyValuePairs = ControllerContext.Request.GetQueryNameValuePairs();
            return appointmentService.GetItems(keyValuePairs);
        }

        [HttpGet]
        public IHttpActionResult GetAppointment(int id)
        {
            var appointment = appointmentService.GetItemById(id, Include.None);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }

        // Comment our for now, easier to test
        // [Authorize(Roles = "Receptionist")]
        [HttpPut]
        public IHttpActionResult EditAppointment(int id, Appointment a)
        {
            appointmentService.UpdateItem(id, a);
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