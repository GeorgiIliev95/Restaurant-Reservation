using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBooking.Models
{
    public class RestaurantReservations
    {
        public int TotalSeats { get; set; }

        public Dictionary<DateTime, int> Reservations { get; set; }

        public RestaurantReservations()
        {
            Reservations = new Dictionary<DateTime, int>();
        }

        public bool AreSeatsEnough(ReservationRequest reservation)
        {
            if (!Reservations.ContainsKey(reservation.Date))
            {
                return true;
            }
            int takenSeats = Reservations[reservation.Date];
            return (TotalSeats - takenSeats) - reservation.PartySize >= 0;
        }
    }
}
