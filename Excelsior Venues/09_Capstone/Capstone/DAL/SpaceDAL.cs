using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{


    public class SpaceDAL
    {
        private string connectionString;
        private string sql_listSpaces = "SELECT id, venue_id, name, is_accessible, ISNULL(open_from, 1) open_from, ISNULL(open_to, 12) open_to, daily_rate, max_occupancy FROM space"; // userInput for venue_id?
        
        IList<Space> spaces = new List<Space>();



        public SpaceDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }




        public IList<Space> VenueSpaces()
        {
            IList<Space> spaces = new List<Space>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();



                    SqlCommand cmd = new SqlCommand(sql_listSpaces, conn);
                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read() == true)
                    {
                        int id = Convert.ToInt32(reader["id"]);
                        int venue_id = Convert.ToInt32(reader["venue_id"]);
                        string name = Convert.ToString(reader["name"]);
                        bool isAccessible = Convert.ToBoolean(reader["is_accessible"]);
                        int openDate = Convert.ToInt32(reader["open_from"]);
                        int closeDate = Convert.ToInt32(reader["open_to"]);
                        double dailyRate = Convert.ToDouble(reader["daily_rate"]);
                        int maxOccupancy = Convert.ToInt32(reader["max_occupancy"]);



                        Space space = new Space(id, venue_id, name, isAccessible, openDate, closeDate, dailyRate, maxOccupancy);

                        spaces.Add(space);


                    }
                }
            }
            catch (Exception ex)
            {

            }

            return spaces;

        }

        


        

    }
}
