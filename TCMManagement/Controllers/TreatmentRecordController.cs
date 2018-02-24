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
    public class TreatmentRecordController : ApiController
    {
        private IEntityServices<TreatmentRecord> treatmentRecordService;
        private readonly IMapper mapper;

        public TreatmentRecordController(IEntityServices<TreatmentRecord> service, IMapper m)
        {
            treatmentRecordService = service;
            mapper = m;
        }

        // Comment our for now, easier to test
        // [Authorize(Roles = "Practitioner")]
        [HttpPost]
        public IHttpActionResult AddRecord(TreatmentCreation t)
        {
            return Ok(treatmentRecordService.CreateItem(mapper.Map<TreatmentRecord>(t)));
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
            var record = treatmentRecordService.GetItemById(id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        // Comment our for now, easier to test
        // [Authorize(Roles = "Practitioner")]
        [HttpPut]
        public IHttpActionResult EditRecord(int id, Delta<TreatmentRecord> t)
        {
            return UpdateTreatmentRecord(id, t);
        }

        [AcceptVerbs("PATCH")]
        public IHttpActionResult PatchPerson(int id, Delta<TreatmentRecord> t)
        {
            return UpdateTreatmentRecord(id, t);
        }

        // This method might need to be moved to business layer.
        private IHttpActionResult UpdateTreatmentRecord(int id, Delta<TreatmentRecord> t)
        {
            // We need to double check email duplication here.
            TreatmentRecord record = treatmentRecordService.GetItemById(id);
            if (record == null)
            {
                return NotFound();
            }

            t.Patch(record);
            treatmentRecordService.SaveChanges();
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
