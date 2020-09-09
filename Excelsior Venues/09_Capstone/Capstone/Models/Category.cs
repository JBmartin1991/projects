using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Category
    {
        public int VenueId { get; set; }
        public string Name { get; set; }

        public Category(int venueId, string name)
        {
            VenueId = venueId;
            Name = name;
        }

    }
}
