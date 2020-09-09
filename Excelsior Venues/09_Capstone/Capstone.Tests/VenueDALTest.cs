using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Tests
{
    [TestClass]
    public class VenueDALTest : ParentTest
    {
        [TestMethod]
        //Test GetVenues()
        public void TestGetVenues()
        {
            VenueDAL newVenue = new VenueDAL(connectionString);
            IList<Venue> venueList = newVenue.GetVenues();

            bool found = false;

            foreach (Venue venue in venueList)
            {
                if (venue.Name == "TestVenue")
                {
                    found = true;
                    break;
                }
            }


            Assert.IsTrue(found);
        }
           

        [TestMethod]
        //Test GetVenueCategories()
        public void TestGetVenueCategories()
        {
            VenueDAL newVenue = new VenueDAL(connectionString);
            IList<Category> categoryList = newVenue.GetVenueCategories();

            bool found = false;

            foreach (Category category in categoryList)
            {
                if (category.Name == "TestCategory")
                {
                    found = true;
                    break;
                }
            }

            
            Assert.IsTrue(found);
        }

        
    }
}
