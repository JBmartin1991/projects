using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class UserInterface
    {

        //ALL Console.ReadLine and WriteLine in this class
        //NONE in any other class

        private VenueDAL venueDAL;
        private SpaceDAL spaceDAL;
        private ReservationDAL reservationDAL;
        private Space space = new Space();
        private Venue venue = new Venue();
        private Reservation reservation = new Reservation();

        private string connectionString;

        public UserInterface(string connectionString)
        {
            this.connectionString = connectionString;
            venueDAL = new VenueDAL(connectionString);
            spaceDAL = new SpaceDAL(connectionString);
            reservationDAL = new ReservationDAL(connectionString);
        }

        bool done = false;
        bool returnToMainMenu = false;
        bool returnToDoNext = false;
        bool returnToReservationInput = false;
        bool areYouDone = false;
        bool completeReservation = false;
        bool exception = false;

        public void Run()
        {

            while (!done)
            {
                returnToMainMenu = false;
                returnToDoNext = false;
                returnToReservationInput = false;
                completeReservation = false;
                exception = false;
                areYouDone = false;

                MainMenu();

                string userinput1 = Console.ReadLine().ToUpper();

                switch (userinput1)
                {
                    case "1":

                        VenueNames();
                        VenueDetails();
                        Console.WriteLine(); // blank line
                        break;

                    case "Q":
                        done = true;
                        return;

                    default:
                        Console.WriteLine("Please enter a valid selection");
                        Console.WriteLine("");
                        returnToMainMenu = true;
                        break;
                }

                while (!returnToMainMenu)
                {
                    returnToDoNext = false;
                    returnToReservationInput = false;
                    completeReservation = false;
                    exception = false;
                    areYouDone = false;

                    DoNext();
                    string userInput = Console.ReadLine().ToUpper();
                    switch (userInput)
                    {

                        case "1":
                            string inputForSpaceToReservation = "";

                            try
                            {
                                DisplaySpaces();    
                            }

                            catch (Exception ex)
                            {
                                returnToDoNext = true;
                                returnToReservationInput = true;
                                completeReservation = true;
                                exception = true;
                                areYouDone = true;

                                Console.WriteLine("");
                                Console.WriteLine("Please enter a valid selection.");
                                Console.WriteLine("");
                                break;
                            }

                            while (!returnToDoNext)
                            {
                                returnToReservationInput = false;
                                completeReservation = false;
                                exception = false;
                                areYouDone = false;

                                SpaceToReservation();
                                Console.WriteLine(); // blank line
                                inputForSpaceToReservation = Console.ReadLine().ToUpper();


                                switch (inputForSpaceToReservation)
                                {
                                    case "1":
                                        ReservationInputAndAvailableSpaces();
                                        break;

                                    case "R":
                                        returnToDoNext = true;
                                        returnToReservationInput = true;
                                        areYouDone = true;
                                        break;

                                    default:
                                        Console.WriteLine("Please enter a valid selection");
                                        Console.WriteLine("");

                                        returnToDoNext = false;
                                        returnToReservationInput = true;
                                        completeReservation = true;
                                        exception = true;
                                        areYouDone = true;

                                        break;
                                }
                            }

                            while (!returnToReservationInput)
                            {
                                completeReservation = false;
                                CompleteReservation();
                            }

                            while (!areYouDone)
                            {
                                AreYouDone();
                            }

                            break;

                        case "2":
                            returnToReservationInput = false;

                            while (!exception)
                            {
                                ReservationInputAndAvailableSpaces();
                            }

                            while (!returnToReservationInput)
                            {
                                completeReservation = false;
                                CompleteReservation();
                            }

                            while (!areYouDone)
                            {
                                AreYouDone();
                            }

                            break;

                        case "R":
                            returnToMainMenu = true;
                            break;

                        default:
                            Console.WriteLine("Please enter a valid selection");
                            Console.WriteLine("");
                            returnToMainMenu = true;
                            break;
                    }
                }
            }
        }

        //Methods

        private int numDays;
        private IList<Space> availableSpaces;

        public void MainMenu()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1) List Venues");
            Console.WriteLine("Q) Quit");
        }

        public void SpaceToReservation()
        {
            Console.WriteLine("What would you like to do next?");
            Console.WriteLine("1) Reserve a Space");
            Console.WriteLine("R) Return to Previous Screen");
           

        }

        public void DoNext()
        {
            Console.WriteLine("What would you like to do next?");
            Console.WriteLine("1) View Spaces");
            Console.WriteLine("2) Search for Reservation");
            Console.WriteLine("R) Return to Previous Screen");
        }

        public void VenueNames()
        {

            Console.WriteLine("Which venue would you like to view?");
            IList<Venue> venues = venueDAL.GetVenues();

            foreach (Venue venue in venues)
            {
                Console.WriteLine(venue.Id + ") " + venue.Name);
            }

        }

        public void VenueDetails()
        {
            try
            {
                string idInput = Console.ReadLine();
                venue.Id = int.Parse(idInput);

                IList<Venue> newListTwo = venueDAL.GetVenues();
                IList<Category> newCategoryList = venueDAL.GetVenueCategories();

                string location = "";
                string description = "";
                string categories = "";
                foreach (Venue room in newListTwo)
                {
                    if (venue.Id < 1 || venue.Id > newListTwo.Count)
                    {
                        Console.WriteLine("Please enter a valid selection");
                        Console.WriteLine("");
                        returnToMainMenu = true;
                        return;
                    }

                    if (venue.Id == room.Id)
                    {

                        Console.WriteLine(); // blank line
                        venue.Name = room.Name;
                        location = room.City + ", " + room.State;
                        description = room.Description;
                        continue;
                    }
                }

                for (int i = 0; i < newCategoryList.Count; i++)
                {
                    if (idInput == newCategoryList[i].VenueId.ToString())
                    {
                        categories += (i == 0 ? "" : " | ") + newCategoryList[i].Name;
                    }
                }

                Console.WriteLine(venue.Name);
                Console.WriteLine("Location: " + location);
                Console.WriteLine("Categories: " + categories);
                Console.WriteLine();
                Console.WriteLine(description);
            }
            catch
            {
                returnToMainMenu = true;
                Console.WriteLine("Please enter a valid selection");
                return;
            }
        }



        public void DisplaySpaces()
        {
            Console.WriteLine("".PadRight(5) + "Name".PadRight(35) + "Opens on (Month)".PadRight(20) + "Closes on (Month)".PadRight(20) + "Daily Rate".PadRight(15) + "Max. Occupancy");
            Console.WriteLine("");
            IList<Space> newList = spaceDAL.VenueSpaces();

            foreach (Space rooms in newList)
            {
                if (venue.Id == rooms.VenueId)
                {
                    space.Id = rooms.Id;
                    space.Name = rooms.Name;
                    space.OpenMonth = rooms.OpenMonth;
                    space.CloseMonth = rooms.CloseMonth;
                    space.DailyRate = rooms.DailyRate;
                    space.MaxOccupancy = rooms.MaxOccupancy;
                    
                    Console.WriteLine(space.Id + "  ".PadRight(5 - space.Id.ToString().Length) + space.Name + "  ".PadRight(35 - space.Name.ToString().Length) + space.OpenMonth + "  ".PadRight(20 - space.OpenMonth.ToString().Length) + space.CloseMonth + "  ".PadRight(20 - space.CloseMonth.ToString().Length) + "$" + space.DailyRate + "  ".PadRight(15 - space.DailyRate.ToString().Length) + space.MaxOccupancy);


                }


            }
        }


        public void ReservationInputAndAvailableSpaces()
        {
            DateTime date;
            int numPeople;
            string dateToString = "";
            try
            {
                Console.WriteLine("What day will you need the space? (MM/DD/YYYY)");
                string dateInput = Console.ReadLine();
                date = DateTime.Parse(dateInput);
                dateToString = date.ToString();

                Console.WriteLine("How many days will you need the space?");
                string days = Console.ReadLine();
                numDays = int.Parse(days);

                Console.WriteLine("How many people will be in attendance?");
                string people = Console.ReadLine();
                numPeople = int.Parse(people);


                DateTime endDate = date.AddDays(numDays);
                string endDateToString = endDate.ToString();
                int startMonth = date.Month;
                int endMonth = endDate.Month;

                reservation.NumberOfAttendees = numPeople;
                reservation.StartDate = date;
                reservation.EndDate = endDate;

                availableSpaces = reservationDAL.AvailableSpaces(dateToString, endDateToString, venue.Id);

                ReturnAvailableSpaceAfterUserInput(availableSpaces, numPeople, date.Month, endDate.Month);

            }
            catch (FormatException ex)
            {
                exception = false;
                completeReservation = true;
                Console.WriteLine("Please enter a valid selection");
                Console.WriteLine("");
                return;
            }
        }


        public IList<Space> ReturnAvailableSpaceAfterUserInput(IList<Space> availableSpaces, int numPeople, int startMonth, int endMonth)
        {

            IList<Space> emptyList = new List<Space>();
            IList<Space> availableSpacesAfterUserInput = new List<Space>();
            Console.WriteLine("");
            Console.WriteLine("Space # " + " ".PadRight(2) + "Name ".PadRight(25) + "Daily Rate ".PadRight(25) + "Max Occup. ".PadRight(25) + "Accessible? ".PadRight(25) + "Total Cost ");
            Console.WriteLine("");

            string result = "";

            int count = 0;

            while (count < 5)
            {
                foreach (Space space in availableSpaces)
                {

                    string yesNo = "No";

                    if (space.IsAccessible == true)
                    {
                        yesNo = "yes";
                    }
                    if (space.MaxOccupancy >= numPeople && (space.OpenMonth <= startMonth && space.CloseMonth >= endMonth))
                    {
                        
                        result = space.Id + " |".PadRight(2) + space.Name + " ".PadRight(25) + " $" + space.DailyRate + " ".PadRight(25 - space.DailyRate.ToString().Length) + space.MaxOccupancy + " ".PadRight(25 - space.MaxOccupancy.ToString().Length) + yesNo + " ".PadRight(25 - yesNo.Length) + " $" + (numDays * space.DailyRate);

                        count++;

                        availableSpacesAfterUserInput.Add(space);

                        Console.WriteLine(result);
                    }

                    if (count == 5)
                    {
                        break;
                    }
                }

                count = 5;
            }

            if (result == "")
            {
                Console.WriteLine("No spaces are available based on your search criteria");

                returnToDoNext = true;
                returnToReservationInput = true;
                returnToMainMenu = true;
                exception = true;
                return emptyList;

            }

            returnToMainMenu = true;
            returnToDoNext = true;
            exception = true;
            Console.WriteLine("");

            return availableSpacesAfterUserInput;
        }

        public void CompleteReservation()
        {
            while (completeReservation != true)
            {

                try
                {

                    Console.WriteLine("Which space would you like to reserve (enter 0 to cancel)?");
                    string stringSpaceIdToReserve = Console.ReadLine();
                    int spaceIdToReserve = int.Parse(stringSpaceIdToReserve);

                    if (spaceIdToReserve == 0)
                    {
                        returnToMainMenu = true;
                        returnToDoNext = true;
                        returnToReservationInput = true;
                        completeReservation = true;
                        done = false;
                        return;
                    }

                    foreach (Space space in availableSpaces)
                    {
                        if (spaceIdToReserve == space.Id)
                        {
                            string nameToReserveUnder = "";
                            Console.WriteLine("Who is this reservation for?");
                            nameToReserveUnder = Console.ReadLine();
                            Reservation newReservation = reservationDAL.ReturnReservation(spaceIdToReserve, reservation.NumberOfAttendees, reservation.StartDate, reservation.EndDate, nameToReserveUnder);

                            Console.WriteLine("");
                            Console.WriteLine("Confirmation #: " + newReservation.ReservationId);
                            Console.WriteLine("Venue: " + venue.Name);
                            Console.WriteLine("Space: " + space.Name);
                            Console.WriteLine("Reserved For: " + nameToReserveUnder);
                            Console.WriteLine("Attendees: " + reservation.NumberOfAttendees);
                            Console.WriteLine("Arrival Date: " + reservation.StartDate);
                            Console.WriteLine("Depart Date: " + reservation.EndDate);
                            Console.WriteLine("Total Cost: $" + (space.DailyRate * numDays));
                            Console.WriteLine("");

                            returnToReservationInput = true;
                            return;
                        }
                    }

                    Console.WriteLine("Please make a valid selection");
                    returnToReservationInput = false;
                    return;

                }


                catch (Exception ex)
                {
                    Console.WriteLine("Please enter a valid selection.");
                    Console.WriteLine("");
                    return;
                }
            }

        }

        public void AreYouDone()
        {
            done = true;
            Console.WriteLine("Press 1 to return to main menu or Q to quit.");
            string lastInput = Console.ReadLine().ToUpper();
            switch (lastInput)
            {
                case "1":
                    returnToDoNext = true;
                    returnToReservationInput = true;
                    returnToMainMenu = true;
                    areYouDone = true;
                    done = false;
                    return;

                case "Q":
                    System.Environment.Exit(1);
                    break;

                default:
                    Console.WriteLine("Please enter a valid selection.");
                    Console.WriteLine("");
                    Console.ReadLine();
                    break;
            }
        }

    }
}
