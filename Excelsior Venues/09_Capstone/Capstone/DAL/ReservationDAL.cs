using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{ 

    public class ReservationDAL
    {
        private string connectionString;

        private string sql_matchingSpaces = "SELECT s.id, s.name, s.daily_rate, s.max_occupancy, s.is_accessible, ISNULL(open_from, 1) open_from, ISNULL(open_to, 12) open_to FROM space s WHERE venue_id = @venue_id AND s.id NOT IN (SELECT s.id from reservation r JOIN space s on r.space_id = s.id " +
                                            "WHERE s.venue_id = @venue_id AND r.end_date >= @start_date AND r.start_date <= @end_date)";
       
        private string sql_addReservation = "INSERT INTO reservation (space_id, number_of_attendees, start_date, end_date, reserved_for) VALUES (@space_id, @guest_amount, @start_date, @end_date, @reservation_name)";

        private Space space;

        IList<Space> spaces = new List<Space>();

        public ReservationDAL(string connectionString)
        {
            this.connectionString = connectionString;
            
        }

        public IList<Space> AvailableSpaces(string dateToString, string endDateToString, int idInput)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql_matchingSpaces, conn);

                    spaces.Clear();

                    cmd.Parameters.AddWithValue("@venue_id", idInput);
                    cmd.Parameters.AddWithValue("@start_date", dateToString);
                    cmd.Parameters.AddWithValue("@end_date", endDateToString);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read() == true)
                    {
                        
                        int id = Convert.ToInt32(reader["id"]);
                        string name = Convert.ToString(reader["name"]);
                        double dailyRate = Convert.ToDouble(reader["daily_rate"]);
                        int maxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                        bool isAccessible = Convert.ToBoolean(reader["is_accessible"]);
                        int openMonth = Convert.ToInt32(reader["open_from"]);
                        int closedMonth = Convert.ToInt32(reader["open_to"]);

                       
                        Space potentialReservation = new Space(id, name, isAccessible, dailyRate, maxOccupancy, openMonth, closedMonth);
                        spaces.Add(potentialReservation);

                    }

                    reader.Close();
                }
            }

            catch (Exception ex)
            {

            }
            return spaces;
        }


        public Reservation ReturnReservation(int spaceIdToReserve, int numPeople, DateTime date, DateTime endDate, string nameToReserveUnder)
        {

            Reservation newReservation = new Reservation();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql_addReservation, conn); //add sql

                    cmd.Parameters.AddWithValue("@space_id", spaceIdToReserve);
                    cmd.Parameters.AddWithValue("@guest_amount", numPeople);
                    cmd.Parameters.AddWithValue("@start_date", date);
                    cmd.Parameters.AddWithValue("@end_date", endDate);
                    cmd.Parameters.AddWithValue("@reservation_name", nameToReserveUnder);

                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT Max(reservation_id) FROM reservation;", conn);
                    newReservation.ReservationId = Convert.ToInt32(cmd.ExecuteScalar());
                }


            }

            catch (Exception ex)
            {

            }

            return newReservation; //return accepted reservation

        }


    }
}
