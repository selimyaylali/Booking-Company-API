using Microsoft.EntityFrameworkCore;
using midterm2.Model; 

namespace Repository
{
    public class HouseAvailabilityRepository
    {
        public bool IsHouseAvailable(int houseId, DateTime checkIn, DateTime checkOut)
        {
            using var context = new SelimContext();
            try
            {
                var house = context.Houses
                    .Include(h => h.Bookings)
                    .FirstOrDefault(h => h.HouseId == houseId);

                if (house == null)
                {
                    return false;
                }

                foreach (var booking in house.Bookings)
                {
                    if (checkIn < booking.ToDate && checkOut > booking.FromDate)
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
