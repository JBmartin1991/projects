using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Space
    {
        public int Id { get; set; }
        public int VenueId { get; set; }
        public string Name { get; set; }
        public bool IsAccessible { get; set; }
        public int? OpenMonth { get; set; }
        public int? CloseMonth { get; set; }
        public double DailyRate { get; set; }
        public int MaxOccupancy { get; set; }

        public Space()
        {

        }

        public Space(int id, int venueId, string name, bool isAccessible, int openMonth, int closeMonth, double dailyRate, int maxOccupancy)
        {
            Id = id;
            VenueId = venueId;
            Name = name;
            IsAccessible = isAccessible;
            OpenMonth = openMonth;
            CloseMonth = closeMonth;
            DailyRate = dailyRate;
            MaxOccupancy = maxOccupancy;
        }

        public Space(int id, string name, bool isAccessible, double dailyRate, int maxOccupancy, int openMonth, int closeMonth)
        {
            Id = id;
            Name = name;
            IsAccessible = isAccessible;
            DailyRate = dailyRate;
            MaxOccupancy = maxOccupancy;
            OpenMonth = openMonth;
            CloseMonth = closeMonth;
        }

    }
}
