using ReportService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Interfaces
{
    public interface IReportRepository
    {
        Task<IEnumerable<Report>> GetAllReportsAsync();
        Task<Report> GetReportByIdAsync(Guid reportId);
        Task AddReportAsync(Report report);
        Task DeleteReportAsync(Guid reportId);
        Task UpdateReportAsync(Report report);
    }
}
