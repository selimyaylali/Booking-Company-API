using Microsoft.EntityFrameworkCore;
using midterm2.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class HousesRepository
    {
        private readonly SelimContext context;

        public HousesRepository(SelimContext context)
        {
            this.context = context;
        }
        
        public House CreateHouse(House house)
        {
            using var context = new SelimContext(); 
            context.Houses.Add(house);
            context.SaveChanges();
            return house;
        }


        public List<House> GetAvailableHouses(HouseQuery query)
        {
            var pagedData = context.Houses
                .Include(h => h.Bookings)
                .Where(house => !house.Bookings.Any(booking => 
                               query.CheckInDate < booking.ToDate && 
                               query.CheckOutDate > booking.FromDate) &&
                               house.MaxGuests >= query.NumberOfPeople)
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToList();

            return pagedData;
        }

        public bool BookHouse(int houseId, Client client, DateTime checkIn, DateTime checkOut)
        {
            try
            {
                var house = context.Houses
                    .Include(h => h.Bookings)
                    .FirstOrDefault(h => h.HouseId == houseId);

                if (house != null && !house.Bookings.Any(booking => 
                    checkIn < booking.ToDate && checkOut > booking.FromDate))
                {
                    var booking = new Booking
                    {
                        ClientId = client.ClientId,
                        HouseId = houseId,
                        FromDate = checkIn,
                        ToDate = checkOut,
                        Status = "Booked"
                    };

                    context.Bookings.Add(booking);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CancelBooking(int bookingId)
        {
            try
            {
                var booking = context.Bookings.Find(bookingId);
                if (booking != null && booking.Status == "Booked")
                {
                    booking.Status = "Cancelled";
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
