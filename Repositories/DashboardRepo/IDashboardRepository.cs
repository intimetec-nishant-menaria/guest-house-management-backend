using guest_house_management_backend.Models;

namespace guest_house_management_backend.Repositories.DashboardRepo
{
    public interface IDashboardRepository
    {
        Task<List<Booking>> GetBookings();
        Task<int> GetTotalRooms();
        Task<int> GetAvailableRooms();
    }
}
