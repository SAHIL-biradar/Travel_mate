using CabBookingDll.Models;
using CabBookingDll.Services;

namespace CabBooking.DataServices
{

    public class CabBookingDataServices
    {
        private readonly ICabBookingServices _cabBookingServices;

        public CabBookingDataServices(ICabBookingServices cabBookingServices)
        {
            _cabBookingServices = cabBookingServices;
        }

        // Asynchronous method to get a specific cab booking
        public Task<Cabbooking> GetCabBookingAsync(int cabBookingId, int currentUserId)
        {
            return Task.Run(() => _cabBookingServices.Get(cabBookingId, currentUserId));
        }

        // Asynchronous method to add a new cab booking
        public Task<int> AddCabBookingAsync(Cabbooking cabBooking, int currentUserId)
        {
            return Task.Run(() => _cabBookingServices.Add(cabBooking, currentUserId));
        }

        // Asynchronous method to delete a cab booking
        public Task DeleteCabBookingAsync(int cabBookingId, int currentUserId)
        {
            return Task.Run(() => _cabBookingServices.Delete(cabBookingId, currentUserId));
        }

        // Asynchronous method to get all cab bookings for a specific user
        public Task<List<Cabbooking>> GetAllCabBookingsAsync(int currentUserId)
        {
            return Task.Run(() => _cabBookingServices.GetAll(currentUserId));
        }

        public Task UpdateCabBookingAsync(Cabbooking cabBooking, BookingStatus status)
        {
            return Task.Run(() => _cabBookingServices.Update(cabBooking, status));
        }

    }
}
