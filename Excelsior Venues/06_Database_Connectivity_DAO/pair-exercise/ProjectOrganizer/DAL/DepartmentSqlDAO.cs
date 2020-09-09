using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOrganizer.DAL
{
    public class DepartmentSqlDAO : IDepartmentDAO
    {
        private string connectionString;
        private string sql_getDepartment = "SELECT * FROM department";
        private string sql_createDepartment = "INSERT INTO department (name) "
            + "VALUES (@name)";
        private string sql_updateDepartment = "UPDATE department SET  name = @name WHERE department_id = @department_id"; //CHECK
           

        // Single Parameter Constructor
        public DepartmentSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        
        }

        /// <summary>
        /// Returns a list of all of the departments.
        /// </summary>
        /// <returns></returns>
        public IList<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql_getDepartment, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read() == true)
                    {
                        int department_Id = Convert.ToInt32(reader["department_id"]);
                        string name = Convert.ToString(reader["name"]);

                        Department department = new Department(department_Id, name);
                        departments.Add(department);
                    }

                }
            }

            catch(Exception ex)
            {
                return departments;
            }

            return departments;
        }

        /// <summary>
        /// Creates a new department.
        /// </summary>
        /// <param name="newDepartment">The department object.</param>
        /// <returns>The id of the new department (if successful).</returns>
        public int CreateDepartment(Department newDepartment)
        {
            int result = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql_createDepartment, conn);

                    cmd.Parameters.AddWithValue("@name", newDepartment.Name);

                    cmd = new SqlCommand("SELECT Max(department_id) FROM department;", conn);
                    result = Convert.ToInt32(cmd.ExecuteScalar());

                }
            }

            catch (Exception ex)
            {
                return result;
            }

            return result;
        }
        
        /// <summary>
        /// Updates an existing department.
        /// </summary>
        /// <param name="updatedDepartment">The department object.</param>
        /// <returns>True, if successful.</returns>
        public bool UpdateDepartment(Department updatedDepartment)
        {

            bool result = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql_updateDepartment, conn);
                    cmd.Parameters.AddWithValue("@name", updatedDepartment.Name);
                    cmd.Parameters.AddWithValue("@department_id", updatedDepartment.Id);
                    int count = cmd.ExecuteNonQuery();
               
                    if (count > 0)
                    {
                        result = true;
                    }



                }
            }

            catch (Exception ex)
            {
                return result;
            }

            return result;
        }

    }
}
