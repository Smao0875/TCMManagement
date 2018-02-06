using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TCMManagement.DataAccessLayer;
using TCMManagement.Models;

namespace TCMManagement.BusinessLayer
{
    public class PatientBusinessLayer
    {
        public Patient GetPatientById(int id)
        {
            PersonBusinessLayer personBL = new PersonBusinessLayer();
            TcmDAL dal = new TcmDAL();
            var patient = dal.Patients.FirstOrDefault((p) => p.PatientId == id);
            patient.Person = personBL.GetPersonById(patient.PersonId);

            if (patient == null)
            {
                return null;
            }
            return patient;
        }


    }
}