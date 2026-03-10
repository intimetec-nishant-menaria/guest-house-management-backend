using guest_house_management_backend.Data;
using guest_house_management_backend.Enums;
using guest_house_management_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace guest_house_management_backend.Repositories.DashboardRepo
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly DBContext _context;
        public DashboardRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<List<Booking>> GetBookings()
        {
            return await _context.Bookings.ToListAsync();
        }
        public async Task<int> GetTotalRooms()
        {
            return await _context.Rooms.CountAsync();
        }
        public async Task<int> GetAvailableRooms()
        {
            return await _context.Rooms
                .Where(r => r.RoomStatus == RoomStatusEnum.Available)
                .CountAsync();
        }
    }
}
