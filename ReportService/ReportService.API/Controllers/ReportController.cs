using Microsoft.AspNetCore.Mvc;
using ReportService.Domain.Interfaces;
using ReportService.Domain.Models;

namespace ReportService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IReportRepository _reportRepository;
        private readonly ILogger<ReportController> _logger;

        public ReportController(IReportRepository reportRepository, ILogger<ReportController> logger)
        {
            _reportRepository = reportRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> GetReports()
        {
            _logger.LogDebug("Fetch All reports");

            var reports = await _reportRepository.GetAllReportsAsync();

            _logger.LogDebug($"Number of reports: {reports.Count()}");

            return Ok(reports);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReportById(Guid id)
        {
            _logger.LogInformation($"Fetch report: {id}");

            var report = await _reportRepository.GetReportByIdAsync(id);

            if (report == null)
            {
                _logger.LogError($"Report '{id}' not found");
                return NotFound();
            }

            _logger.LogInformation($"Report '{id}' fetched successfully");

            return Ok(report);
        }

        [HttpPost]
        public async Task<ActionResult> AddReport(Report report)
        {
            _logger.LogDebug("Report received: {@Report}", report);

            await _reportRepository.AddReportAsync(report);

            _logger.LogInformation("Report added successfully");

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReport(Report report)
        {
            _logger.LogDebug("Update report: {@Report}", report);

            if (report == null)
            {
                _logger.LogError($"Report to update is not found");
                return BadRequest();
            }

            _logger.LogInformation("Report updated successfully");

            await _reportRepository.UpdateReportAsync(report);

            return Ok(report);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReport(Guid id)
        {
            _logger.LogDebug($"Delete report {id}");

            await _reportRepository.DeleteReportAsync(id);

            _logger.LogInformation($"Report '{id}' deleted successfully");

            return NoContent();
        }
    }
}
