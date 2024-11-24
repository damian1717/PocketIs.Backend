using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using PocketIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class ComplaintService : IComplaintService
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly IDocumentService _documentService;
        public ComplaintService(IComplaintRepository complaintRepository,
            IDocumentService documentService)
        {
            _complaintRepository = complaintRepository;
            _documentService = documentService;
        }
        public async Task AddComplaintAsync(Complaint complaint)
            => await _complaintRepository.AddComplaintAsync(complaint);

        public async Task<Complaint> GetComplaintByIdAsync(Guid id)
            => await _complaintRepository.GetComplaintByIdAsync(id);

        public async Task<List<ComplaintInfo>> GetComplaintsAsync(DateTime? dateFrom, DateTime? dateTo)
        {
            var complaints = await _complaintRepository.GetComplaintsAsync(dateFrom, dateTo);

            if (complaints is null || complaints.Count <= 0) return new List<ComplaintInfo>();

            var complaintIds = complaints.Select(x => $"COM_{x.Id}").ToList();

            var documents = await _documentService.GetAllDocumentsByCodesAsync(complaintIds);

            var items = new List<ComplaintInfo>();
            foreach (var complaint in complaints)
            {
                var allDoc = documents.Where(x => x.Code.EndsWith(complaint.Id.ToString())).Select(d => d.Name).ToList();
                var filesName = string.Join(',', allDoc);

                var item = new ComplaintInfo
                {
                    Id = complaint.Id,
                    Type = complaint.Type,
                    Status = complaint.Status,
                    Client = complaint.Client,
                    Product = complaint.Product,
                    Date = complaint.Date,
                    Deadline = complaint.Deadline,
                    ResponsiblePerson = complaint.ResponsiblePerson,
                    Actions = complaint.Actions,
                    WhatHappened = complaint.WhatHappened,
                    WhyItIsProblem = complaint.WhyItIsProblem,
                    WhenProblemIdentified = complaint.WhenProblemIdentified,
                    WhereProblemDetected = complaint.WhereProblemDetected,
                    HowProblemDetected = complaint.HowProblemDetected,
                    WhoProblemDetected = complaint.WhoProblemDetected,
                    PiecesNok = complaint.PiecesNok,
                    ProperProcess = complaint.ProperProcess,
                    InconsistencyDetected = complaint.InconsistencyDetected,
                    FileNames = filesName
                };

                items.Add(item);
            }

            return items;
        }

        public async Task UpdateComplaintAsync(Complaint complaint)
            => await _complaintRepository.UpdateComplaintAsync(complaint);

        public async Task DeleteComplaintAsync(Guid id)
            => await _complaintRepository.DeleteComplaintAsync(id);
    }
}
