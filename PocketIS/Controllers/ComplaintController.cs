using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocketIS.Domain;
using PocketIS.Models.Complaint;
using PocketIS.Services.Interfaces;

namespace PocketIS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ComplaintController : BaseController
    {
        private readonly IComplaintService _complaintService;
        public ComplaintController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }

        [HttpPost]
        [Route("addcomplaint")]
        public async Task<IActionResult> AddComplaint(AddComplaint model)
        {
            var complaint = new Complaint
            {
                Type = model.Type,
                Status = model.Status,
                Client = model.Client,
                Product = model.Product,
                Date = model.Date,
                Deadline = model.Deadline,
                ResponsiblePerson = model.ResponsiblePerson,
                Actions = model.Actions,
                WhatHappened = model.WhatHappened,
                WhyItIsProblem = model.WhyItIsProblem,
                WhenProblemIdentified = model.WhenProblemIdentified,
                WhereProblemDetected = model.WhereProblemDetected,
                HowProblemDetected = model.HowProblemDetected,
                WhoProblemDetected = model.WhoProblemDetected,
                PiecesNok = model.PiecesNok,
                ProperProcess = model.ProperProcess,
                InconsistencyDetected = model.InconsistencyDetected,
                CompanyId = CompanyId
            };

            await _complaintService.AddComplaintAsync(complaint);
            return Ok();
        }

        [HttpPost]
        [Route("updatecomplaint")]
        public async Task<IActionResult> UpdateComplaint(UpdateComplaint model)
        {
            if (model?.Id is null) return BadRequest();

            var complaint = new Complaint
            {
                Id = model.Id,
                Type = model.Type,
                Status = model.Status,
                Client = model.Client,
                Product = model.Product,
                Date = model.Date,
                Deadline = model.Deadline,
                ResponsiblePerson = model.ResponsiblePerson,
                Actions = model.Actions,
                WhatHappened = model.WhatHappened,
                WhyItIsProblem = model.WhyItIsProblem,
                WhenProblemIdentified = model.WhenProblemIdentified,
                WhereProblemDetected = model.WhereProblemDetected,
                HowProblemDetected = model.HowProblemDetected,
                WhoProblemDetected = model.WhoProblemDetected,
                PiecesNok = model.PiecesNok,
                ProperProcess = model.ProperProcess,
                InconsistencyDetected = model.InconsistencyDetected,
                CompanyId = CompanyId,

            };

            await _complaintService.UpdateComplaintAsync(complaint);
            return Ok();
        }

        [HttpGet]
        [Route("getcomplaints/{dateFrom}/{dateTo}")]
        public async Task<IActionResult> Get(string dateFrom, string dateTo)
        {
            var from = GetDate(dateFrom);
            var to = GetDate(dateTo);

            return Ok(await _complaintService.GetComplaintsAsync(from, to));
        }

        [HttpGet]
        [Route("getcomplaintbyid/{id}")]
        public async Task<IActionResult> Get(Guid id) => Ok(await _complaintService.GetComplaintByIdAsync(id));

        [HttpDelete]
        [Route("deletecomplaint/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _complaintService.DeleteComplaintAsync(id);
            return Ok();
        }

        private DateTime? GetDate(string date)
        {
            if (string.IsNullOrWhiteSpace(date)) return null;

            if (DateTime.TryParse(date, out DateTime value)) return value;

            return null;
        }
    }
}
