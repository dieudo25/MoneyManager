using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReportService.Database.Context;
using ReportService.Domain.Interfaces;
using ReportService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Database.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ReportDbContext? _dbContext;
        private readonly ILogger<ReportRepository> _logger;

        public ReportRepository(ReportDbContext dbContext, ILogger<ReportRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task AddReportAsync(Report report)
        {
            await _dbContext.Reports.AddAsync(report);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteReportAsync(Guid id)
        {
            var report = await _dbContext.Reports.FindAsync(id);

            if (report != null)
            {
                _dbContext.Reports.Remove(report);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new NullReferenceException($"Report {{{id}}} not found. Delete failed");
            }
        }

        public async Task<IEnumerable<Report>> GetAllReportsAsync()
        {
            return await _dbContext.Reports.ToListAsync();
        }

        public async Task<Report> GetReportByIdAsync(Guid id)
        {
            return await _dbContext.Reports.SingleOrDefaultAsync(t => t.Id == id);
        }

        public Task UpdateReportAsync(Report report)
        {
            var existingReport = _dbContext.Reports.FirstOrDefault(t => t.Id == report.Id);

            if (existingReport != null)
            {
                existingReport.Name = report.Name;
                existingReport.CategoryId = report.CategoryId;
                existingReport.StartDate = report.StartDate;
                existingReport.EndDate = report.EndDate;
            }

            return Task.CompletedTask;
        }
    }
}
