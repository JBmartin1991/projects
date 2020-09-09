using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Reservation
    {
        private int id;
        private string name;
        private decimal daily_rate;
        private int max_occupancy;
        private bool is_accessible;

        public int ReservationId { get; set; }
        public int SpaceId { get; set; }
        public int NumberOfAttendees { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ReservedFor { get; set; }
        public int OpenMonth { get; set; }
        public int ClosedMonth { get; set; }
        

        public Reservation(int reservationId, int spaceId, int numberOfAttendees, DateTime startDate, DateTime endDate, string reservedFor)
        {
            ReservationId = reservationId;
            SpaceId = spaceId;
            NumberOfAttendees = numberOfAttendees;
            StartDate = startDate;
            EndDate = endDate;
            ReservedFor = reservedFor;
        }

        public Reservation(int id, string name, decimal daily_rate, int max_occupancy, bool is_accessible, int closedMonth, int openMonth)
        {
            SpaceId = id;
            this.name = name;
            this.daily_rate = daily_rate;
            this.max_occupancy = max_occupancy;
            this.is_accessible = is_accessible;
            OpenMonth = openMonth;
            ClosedMonth = closedMonth;
        }

        public Reservation()
        {
        }
    }
}
