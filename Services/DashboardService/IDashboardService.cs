using guest_house_management_backend.DTOs;

namespace guest_house_management_backend.Services.DashboardService
{
    public interface IDashboardService
    {
        Task<DashboardStatsDto> GetDashboardStats(DateTime? startDate, DateTime? endDate);
    }
}
