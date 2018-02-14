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
    public class TreatmentRecordController : ApiController
    {
        private IEntityServices<TreatmentRecord> treatmentRecordService;

        public TreatmentRecordController() {
            treatmentRecordService = new TreatmentRecordService();
        }

        public TreatmentRecordController(IEntityServices<TreatmentRecord> service)
        {
            treatmentRecordService = service;
        }

        // Comment our for now, easier to test
        // [Authorize(Roles = "Practitioner")]
        [HttpPost]
        public IHttpActionResult AddRecord(TreatmentRecord t)
        {
            return Ok(treatmentRecordService.CreateItem(t));
        }

        // querystring = "?Patient=1"
        [HttpGet]
        public IEnumerable<TreatmentRecord> GetRecords()
        {
            var keyValuePairs = ControllerContext.Request.GetQueryNameValuePairs();
            return treatmentRecordService.GetItems(keyValuePairs);
        }

// This method might never get called.
// Maybe we should delete it.
        [HttpGet]
        public IHttpActionResult GetRecord(int id)
        {
            var record = treatmentRecordService.GetItemById(id, Include.None);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        // Comment our for now, easier to test
        // [Authorize(Roles = "Practitioner")]
        [HttpPut]
        public IHttpActionResult EditRecord(int id, TreatmentRecord t)
        {
            treatmentRecordService.UpdateItem(id, t);
            return Ok(id);
        }

// Need to change to softDelete
        // [Authorize(Roles = "Practitioner")]
        [HttpDelete]
        public IHttpActionResult DeleteRecord(int id)
        {
            if(treatmentRecordService.DeleteItem(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
