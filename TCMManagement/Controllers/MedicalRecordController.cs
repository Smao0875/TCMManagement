using AutoMapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using TCMManagement.BusinessLayer;
using TCMManagement.DTOs;
using TCMManagement.Models;

namespace TCMManagement.Controllers
{
    /// <summary>
    /// All Controllers should follow CRUD sequence so it will be easier to maintain the code.
    /// </summary>
    public class MedicalRecordController : ApiController
    {
        private IEntityServices<MedicalHistoryRecord> medicalRecordService;
        private readonly IMapper mapper;

        public MedicalRecordController(IEntityServices<MedicalHistoryRecord> service, IMapper m)
        {
            medicalRecordService = service;
            mapper = m;
        }

        // Comment our for now, easier to test
        // [Authorize(Roles = "Practitioner")]
        [HttpPost]
        public IHttpActionResult AddRecord(MedicalRecordCreation m)
        {
            return Ok(medicalRecordService.CreateItem(mapper.Map<MedicalHistoryRecord>(m)));
        }

        // querystring = "?Patient=1"
        [HttpGet]
        public IEnumerable<MedicalHistoryRecord> GetRecords()
        {
            var keyValuePairs = ControllerContext.Request.GetQueryNameValuePairs();
            return medicalRecordService.GetItems(keyValuePairs);
        }

        [HttpGet]
        public IHttpActionResult GetRecord(int id)
        {
            var record = medicalRecordService.GetItemById(id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        // Comment our for now, easier to test
        // [Authorize(Roles = "Practitioner")]
        [HttpPut]
        public IHttpActionResult EditRecord(int id, Delta<MedicalHistoryRecord> m)
        {
            return UpdateMedicalHistoryRecord(id, m);
        }

        // Comment our for now, easier to test
        // [Authorize(Roles = "Practitioner")]
        [AcceptVerbs("PATCH")]
        public IHttpActionResult PatchRecord(int id, Delta<MedicalHistoryRecord> m)
        {
            return UpdateMedicalHistoryRecord(id, m);
        }

        // This method might need to be moved to business layer.
        private IHttpActionResult UpdateMedicalHistoryRecord(int id, Delta<MedicalHistoryRecord> m)
        {
            // We need to double check email duplication here.
            MedicalHistoryRecord record = medicalRecordService.GetItemById(id);
            if (record == null)
            {
                return NotFound();
            }

            m.Patch(record);
            medicalRecordService.SaveChanges();
            return Ok(id);
        }

        // [Authorize(Roles = "Practitioner")]
        [HttpDelete]
        public IHttpActionResult DeleteRecord(int id)
        {
            if(medicalRecordService.DeleteItem(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
