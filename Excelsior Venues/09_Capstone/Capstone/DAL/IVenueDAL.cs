using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    interface IVenueDAL
    {

        IList<Venue> ListVenues();

        string VenueDetails(Venue venue);


    }
}
