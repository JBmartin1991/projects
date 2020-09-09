using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Tests
{
    [TestClass]
    public class UserInterfaceTest : ParentTest
    {
        public object ICollectionAssert { get; private set; }

        [TestMethod]
        public void ReturnAvailableSpacesAfterUserInputTest()
        {
            UserInterface spaceAfterInput = new UserInterface(connectionString);
            ReservationDAL spaceBeforeInput = new ReservationDAL(connectionString);

            IList<Space> availableSpacesBeforeUserInput = spaceBeforeInput.AvailableSpaces("6/20/2020", "6/24/2020", 99);

            IList<Space> availableSpacesAfterInputTest = spaceAfterInput.ReturnAvailableSpaceAfterUserInput(availableSpacesBeforeUserInput, 10, 6, 6);


            bool found1 = false;
            bool found2 = false;
            bool found3 = false;

            foreach (Space space in availableSpacesBeforeUserInput)
            {
                if (space.Name == "TestSpace")
                {
                    found1 = true;
                }

                if (space.Name == "TestSpaceUserInput")
                {
                    found2 = true;
                }
                
            }

            if (found1 && found2)
            {
                found3 = true;
            }

            Assert.IsTrue(found3);



           

            Assert.AreNotEqual(availableSpacesBeforeUserInput.Count, availableSpacesAfterInputTest.Count);


        }
    }
}
