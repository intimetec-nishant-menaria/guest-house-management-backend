using guest_house_management_backend.DTOs;
using guest_house_management_backend.Enums;
using guest_house_management_backend.Repositories.DashboardRepo;

namespace guest_house_management_backend.Services.DashboardService
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;
        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }
        public async Task<DashboardStatsDto> GetDashboardStats(DateTime? startDate, DateTime? endDate)
        {
            var bookings = await _dashboardRepository.GetBookings();
            var totalRooms = await _dashboardRepository.GetTotalRooms();
            var availableRooms = await _dashboardRepository.GetAvailableRooms();
            var today = DateTime.Today;
            var todayCheckIns = bookings
                .Count(b => b.ActualCheckInTime.HasValue &&
                            b.ActualCheckInTime.Value.Date == today);

            var todayCheckOuts = bookings
                .Count(b => b.ActualCheckOutTime.HasValue &&
                            b.ActualCheckOutTime.Value.Date == today);

            var todayRevenue = bookings
                .Where(b => b.ActualCheckOutTime.HasValue &&
                            b.ActualCheckOutTime.Value.Date == today)
                .Sum(b => b.FinalBillAmount ?? 0);

            var dailyRevenue = bookings
                .Where(b => b.CreatedAt.Date == today)
                .Sum(b => b.FinalBillAmount ?? 0);

            var weeklyRevenue = bookings
                .Where(b => b.CreatedAt >= today.AddDays(-7))
                .Sum(b => b.FinalBillAmount ?? 0);

            var monthlyRevenue = bookings
                .Where(b => b.CreatedAt.Month == today.Month &&
                            b.CreatedAt.Year == today.Year)
                .Sum(b => b.FinalBillAmount ?? 0);

            var occupiedRooms = bookings
                .Count(b => b.Status == BookingStatusEnum.CheckedIn);

            double occupancyRate = 0;

            if (totalRooms > 0)
            {
                occupancyRate = ((double)occupiedRooms / totalRooms) * 100;
            }

            return new DashboardStatsDto
            {
                OccupancyRate = occupancyRate,
                TodayRevenue = todayRevenue,
                AvailableRooms = availableRooms,
                TodayCheckIns = todayCheckIns,
                TodayCheckOuts = todayCheckOuts,
                DailyRevenue = dailyRevenue,
                WeeklyRevenue = weeklyRevenue,
                MonthlyRevenue = monthlyRevenue
            };
        }
    }
}
