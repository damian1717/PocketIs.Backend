using Microsoft.EntityFrameworkCore;
using PocketIS.Application.Common.Interfaces;
using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketIS.Repositories
{
    public class ComplaintRepository : BaseRepository, IComplaintRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public ComplaintRepository(IUserProvider userProvider, IApplicationDbContext dbContext)
            : base(userProvider)
        {
            _dbContext = dbContext;
        }
        public async Task AddComplaintAsync(Complaint complaint)
        {
            complaint.InsertedDate = DateTime.Now;
            complaint.InsertedUserId = UserId;
            complaint.UpdatedDate = DateTime.Now;
            complaint.UpdatedUserId = UserId;

            await _dbContext.Complaints.AddAsync(complaint);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Complaint> GetComplaintByIdAsync(Guid id)
            => await _dbContext.Complaints
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<Complaint>> GetComplaintsAsync(DateTime? dateFrom, DateTime? dateTo)
        {
            IQueryable<Complaint> query = null;

            if (dateFrom is not null && dateTo is not null)
            {
                query = _dbContext.Complaints
                    .Where(x => x.CompanyId == CompanyId)
                    .Where(x => x.Date >= dateFrom && x.Date <= dateTo)
                    .OrderByDescending(x => x.Date);
            } 
            else if (dateFrom is not null)
            {
                query = _dbContext.Complaints
                    .Where(x => x.CompanyId == CompanyId)
                    .Where(x => x.Date >= dateFrom)
                    .OrderByDescending(x => x.Date);
            }
            else if (dateTo is not null)
            {
                query = _dbContext.Complaints
                    .Where(x => x.CompanyId == CompanyId)
                    .Where(x => x.Date <= dateTo)
                    .OrderByDescending(x => x.Date);
            }
            else
            {
                query = _dbContext.Complaints
                    .Where(x => x.CompanyId == CompanyId)
                    .OrderByDescending(x => x.Date);
            }

            return await query.ToListAsync();
        }

        public async Task UpdateComplaintAsync(Complaint complaint)
        {
            var currentComplaint = _dbContext.Complaints.Find(complaint.Id);

            if (currentComplaint is not null)
            {
                currentComplaint.Type = complaint.Type;
                currentComplaint.Status = complaint.Status;
                currentComplaint.Client = complaint.Client;
                currentComplaint.Product = complaint.Product;
                currentComplaint.Date = complaint.Date;
                currentComplaint.Deadline = complaint.Deadline;
                currentComplaint.ResponsiblePerson = complaint.ResponsiblePerson;
                currentComplaint.Actions = complaint.Actions;
                currentComplaint.WhatHappened = complaint.WhatHappened;
                currentComplaint.WhyItIsProblem = complaint.WhyItIsProblem;
                currentComplaint.WhenProblemIdentified = complaint.WhenProblemIdentified;
                currentComplaint.WhereProblemDetected = complaint.WhereProblemDetected;
                currentComplaint.HowProblemDetected = complaint.HowProblemDetected;
                currentComplaint.WhoProblemDetected = complaint.WhoProblemDetected;
                currentComplaint.PiecesNok = complaint.PiecesNok;
                currentComplaint.ProperProcess = complaint.ProperProcess;
                currentComplaint.InconsistencyDetected = complaint.InconsistencyDetected;
                currentComplaint.UpdatedDate = DateTime.Now;
                currentComplaint.UpdatedUserId = UserId;

                _dbContext.Complaints.Update(currentComplaint);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteComplaintAsync(Guid id)
        {
            var complaint = new Complaint()
            {
                Id = id
            };

            _dbContext.Complaints.Remove(complaint);
            await _dbContext.SaveChangesAsync();
        }
    }
}
