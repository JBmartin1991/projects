using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Tests
{
    [TestClass]
    public class SpaceDALTest : ParentTest
    {

        [TestMethod]
        //Test VenueSpaces()
        public void TestVenueSpaces()
        {
            SpaceDAL newSpace = new SpaceDAL(connectionString);
            //Space testSpace = new Space(99, 99, "TestSpace", false, 1, 12, 1000, 1000);
            IList<Space> spaceList = newSpace.VenueSpaces();

            bool found = false;

            foreach (Space place in spaceList)
            {
                if (place.Name == "TestSpace")
                {
                    found = true;
                    break;
                }
            }


            Assert.IsTrue(found);
        }

       
    }
}
