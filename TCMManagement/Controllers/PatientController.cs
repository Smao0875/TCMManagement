using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TCMManagement.Models;

namespace TCMManagement.Controllers
{
    public class PatientController : ApiController
    {
        Patient[] patients = new Patient[]
        {
            new Patient { Id = 1, FirstName = "Bob", LastName = "Dylen", Address = "London", DateOfBirth = "2000-01-01", Email= "bob@gmail.com", Gender = "M", Phone = "1111111111" },
            new Patient { Id = 2, FirstName = "Allen", LastName = "Dylen", Address = "London", DateOfBirth = "2000-01-01", Email= "bob@gmail.com", Gender = "M", Phone = "1111111111" },
            new Patient { Id = 3, FirstName = "Rob", LastName = "Dylen", Address = "London", DateOfBirth = "2000-01-01", Email= "bob@gmail.com", Gender = "M", Phone = "1111111111" },
            new Patient { Id = 4, FirstName = "Alice", LastName = "Dylen", Address = "London", DateOfBirth = "2000-01-01", Email= "bob@gmail.com", Gender = "M", Phone = "1111111111" },
        };

        public IEnumerable<Patient> GetAllPatients()
        {
            return patients;
        }

        public IHttpActionResult GetPatient(int id)
        {
            var patient = patients.FirstOrDefault((p) => p.Id == id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }
    }
}
