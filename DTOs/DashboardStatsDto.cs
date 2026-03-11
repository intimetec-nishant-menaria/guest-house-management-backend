namespace guest_house_management_backend.DTOs
{
    public class DashboardStatsDto
    {
        public double OccupancyRate { get; set; }
        public decimal TodayRevenue { get; set; }
        public int AvailableRooms { get; set; }
        public int TodayCheckIns { get; set; }
        public int TodayCheckOuts { get; set; }
        public decimal DailyRevenue { get; set; }
        public decimal WeeklyRevenue { get; set; }
        public decimal MonthlyRevenue { get; set; }
    }
}
