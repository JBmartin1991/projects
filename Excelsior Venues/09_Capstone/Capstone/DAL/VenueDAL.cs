using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class VenueDAL
    {
        private string connectionString;
        private string sql_listVenues = "SELECT v.id, v.name, v.description, c.name as city, c.state_abbreviation FROM venue v" +
            " JOIN city c ON v.city_id = c.id";
        private string sql_listVenueCategories = "SELECT v.id as venue_id, c.name FROM venue v JOIN category_venue cv ON v.id = cv.venue_id" +
            " JOIN category c ON cv.category_id = c.id";

        IList<Venue> venues = new List<Venue>();
        IList<Venue> venueCategories = new List<Venue>();

        public VenueDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }



        public IList<Venue> GetVenues()
        {
            IList<Venue> venues = new List<Venue>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql_listVenues, conn);
                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read() == true)
                    {
                        int id = Convert.ToInt32(reader["id"]);
                        string name = Convert.ToString(reader["name"]);
                        string description = Convert.ToString(reader["description"]);
                        string city = Convert.ToString(reader["city"]);
                        string state = Convert.ToString(reader["state_abbreviation"]);


                        Venue venue = new Venue(id, name, description, city, state);
                        venues.Add(venue);

                    }

                    reader.Close(); //unecessary


                }


            }

            catch (Exception ex)
            {

            }

            return venues;

        }

        //public IList<Venue> GetVenues()
        //{
        //    return venues;
        //}

        public IList<Category> GetVenueCategories() // create category model, copy code above to get categories
        {
            IList<Category> categories = new List<Category>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql_listVenueCategories, conn);
                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read() == true)
                    {
                        int venueId = Convert.ToInt32(reader["venue_id"]);
                        string name = Convert.ToString(reader["name"]);



                        Category category = new Category(venueId, name);
                        categories.Add(category);

                    }

                    reader.Close();



                }


            }

            catch (Exception ex)
            {
                return null;
            }

            return categories;
        }




        //public string VenueDetails(Venue venue)
        //{


        //    string venueDetails = venue.Name + "\n" + "Location: " + venue.City + ", " + venue.State + "\n" + "Categories: " + venue.Categories + "\n" + "\n" + venue.Description;

        //    return venueDetails;
        //}




    }
}
