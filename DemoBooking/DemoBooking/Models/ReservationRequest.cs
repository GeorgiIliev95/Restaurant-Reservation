using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBooking.Models
{
    public class ReservationRequest
    {
        public ReservationRequest()
        {
        }

        public int PartySize { get; set; }

        public DateTime Date { get; set; }

    }
}
