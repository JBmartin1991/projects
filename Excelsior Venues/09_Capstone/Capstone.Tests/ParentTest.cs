using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Transactions;

namespace Capstone.Tests
{
    [TestClass]
    public class ParentTest
    {
        private TransactionScope trans;

        protected string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=excelsior_venues;Integrated Security=True";

        [TestInitialize]
        public void Setup()
        {
            trans = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql_insert2 = "SET IDENTITY_INSERT venue ON INSERT INTO venue (id, name, city_id, description)" +
                   " VALUES (99, 'TestVenue', 2, 'THIS IS A TEST') Set IDENTITY_INSERT venue OFF;";
                SqlCommand cmd2 = new SqlCommand(sql_insert2, conn);
                cmd2.ExecuteNonQuery();

                string sql_insert = "INSERT INTO space (venue_id, name, is_accessible, open_from, open_to, daily_rate, max_occupancy)" +
                    " VALUES (99, 'TestSpace', 0, 1, 4, 1000, 1000);";
                SqlCommand cmd = new SqlCommand(sql_insert, conn);
                cmd.ExecuteNonQuery();

                string sql_insert4 = "INSERT INTO space (venue_id, name, is_accessible, open_from, open_to, daily_rate, max_occupancy)" +
                   " VALUES (99, 'TestSpaceUserInput', 0, 6, 12, 1000, 1000);";
                SqlCommand cmd4 = new SqlCommand(sql_insert4, conn);
                cmd4.ExecuteNonQuery();

                string sql_insert3 = "SET IDENTITY_INSERT category ON INSERT INTO category (id, name)" +
                    " VALUES (7, 'TestCategory') SET IDENTITY_INSERT category OFF; INSERT INTO category_venue (venue_id, category_id) VALUES (15,7)";
                SqlCommand cmd3 = new SqlCommand(sql_insert3, conn);
                cmd3.ExecuteNonQuery();
            }
        }


        [TestCleanup]
        public void Reset()
        {
            trans.Dispose();
        }

    }
}

