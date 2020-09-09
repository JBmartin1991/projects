using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Tests
{
    [TestClass]
   public class ReservationDALTest : ParentTest
    {
        [TestMethod]
        //Test AvailableSpaces()
        public void AvailableSpacesTest()
        {
            ReservationDAL potentialReservation = new ReservationDAL(connectionString);
            IList<Space> spaceList = potentialReservation.AvailableSpaces("08/20/2020", "08/25/2020", 99);

            bool found = false;

            foreach(Space place in spaceList)
            {
                if(place.Name == "TestSpace")
                {
                    found = true;
                    break;
                }
            }

            Assert.IsTrue(found);

         }
        [TestMethod]
        //Test ReturnReservation()
        public void TestReturnReservation()
        {
            ReservationDAL newReservation = new ReservationDAL(connectionString);
            DateTime startDate = new DateTime(2020, 06, 20);
            DateTime endDate = new DateTime(2020, 06, 25);
            Reservation newRes = newReservation.ReturnReservation(15, 800, startDate, endDate, "TEST");
            Reservation toTestAgainst = new Reservation(newRes.ReservationId, 15, 800, startDate, endDate, "TEST");

            string newRSVP = newRes.ToString();
            string test = toTestAgainst.ToString();

            Assert.AreEqual(newRSVP, test);
        }
    }
}
