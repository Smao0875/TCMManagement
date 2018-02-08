using System.Data.Entity;
using System.Linq;
using TCMManagement.DataAccessLayer;
using TCMManagement.Models;

namespace TCMManagement.BusinessLayer
{
    public class PatientBusinessLayer
    {

        public Patient GetPatientById(int id)
        {
            using(var context = new TcmDAL())
            {
                var patient = context.Patients
                                 .Where(p => p.PatientId == id)
                                 .Include(p => p.Person)
                                 .FirstOrDefault();

                return patient;
            }
        }


    }
}