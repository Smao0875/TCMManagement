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
    public class MedicalRecordController : ApiController
    {
        private IEntityServices<MedicalHistoryRecord> medicalRecordService;

        public MedicalRecordController() {
            medicalRecordService = new MedicalRecordService();
        }

        public MedicalRecordController(IEntityServices<MedicalHistoryRecord> service)
        {
            medicalRecordService = service;
        }

        // Comment our for now, easier to test
        // [Authorize(Roles = "Practitioner")]
        [HttpPost]
        public IHttpActionResult AddRecord(MedicalHistoryRecord m)
        {
            return Ok(medicalRecordService.CreateItem(m));
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
            var record = medicalRecordService.GetItemById(id, Include.None);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        // Comment our for now, easier to test
        // [Authorize(Roles = "Practitioner")]
        [HttpPut]
        public IHttpActionResult EditRecord(int id, MedicalHistoryRecord m)
        {
            medicalRecordService.UpdateItem(id, m);
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
