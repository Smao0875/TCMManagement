using AutoMapper;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using TCMManagement.BusinessLayer;
using TCMManagement.ErrorHelper;
using TCMManagement.Models;

namespace TCMManagement.Controllers
{
    /// <summary>
    /// All Controllers should follow CRUD sequence so it will be easier to maintain the code.
    /// </summary>
    public class PatientController : ApiController
    {
        private readonly IEntityServices<Patient> patientService;

        public PatientController()
        {
            patientService = new PatientService();
        }

        public PatientController(IEntityServices<Patient> service)
        {
            patientService = service;
        }

        // Comment our for now, easier to test
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public IHttpActionResult AddPatient(Patient p)
        {
            if (IsEmailExist(p.Email))
            {
                return Conflict(); // "This email is already taken by others."
            }

            return Ok(patientService.CreateItem(p));
        }

        // querystring = "?type=practitioner"
        [HttpGet]
        public IEnumerable<Patient> GetPatients()
        {
            var keyValuePairs = ControllerContext.Request?.GetQueryNameValuePairs();
            return patientService.GetItems(keyValuePairs);
        }

        [HttpGet]
        public IHttpActionResult GetPatient(int id)
        {
            var patient = patientService.GetItemById(id);
            if (patient == null)
            {
                throw new ApiDataException(1001, "No product found for this id.", HttpStatusCode.NotFound);
            }
            return Ok(patient);
        }

        // Comment our for now, easier to test
        // [Authorize(Roles = "admin")]
        [HttpPut]
        public IHttpActionResult EditPatient(int id, Delta<Patient> p)
        {
            return UpdatePatient(id, p);
        }

        [AcceptVerbs("PATCH")]
        public IHttpActionResult PatchPatient(int id, Delta<Patient> p)
        {
            return UpdatePatient(id, p);
        }

        //[Authorize(Roles = "admin")]
        [HttpDelete]
        public IHttpActionResult DeletePatient(int id)
        {
            if (patientService.DeleteItem(id))
            {
                return Ok();
            }
            //return NotFound();
            throw new ApiDataException(1001, "No product found for this id.", HttpStatusCode.NotFound);
        }

        #region Helper
        private bool IsEmailExist(string email)
        {
            if (patientService.SearchItem(email) != null)
            {
                return true;
            }

            return false;
        }

        private IHttpActionResult UpdatePatient(int id, Delta<Patient> p)
        {
            // NOTE: Email can't be updated!!!
            Patient patient = patientService.GetItemById(id);
            if (patient == null)
            {
                return NotFound();
            }

            p.Patch(patient);
            patientService.SaveChanges();
            return Ok(id);
        }
        #endregion
    }
}
