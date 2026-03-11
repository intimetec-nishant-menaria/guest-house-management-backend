using guest_house_management_backend.Services.DashboardService;
using Microsoft.AspNetCore.Mvc;

namespace guest_house_management_backend.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetDashboardStats( DateTime? startDate, DateTime? endDate)
        {
            var stats = await _dashboardService
                .GetDashboardStats(startDate, endDate);
            return Ok(stats);
        }
    }
}