using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TCMManagement.BusinessLayer;
using TCMManagement.DataAccessLayer;
using TCMManagement.Models;

namespace TCMManagement.Controllers
{
    public class PatientController : ApiController
    {
        [HttpPost]
        public IHttpActionResult AddPatient(Patient p)
        {
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetPatient(int id)
        {
            PatientBusinessLayer bl = new PatientBusinessLayer();
            Patient patient = bl.GetPatientById(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }
    }
}
