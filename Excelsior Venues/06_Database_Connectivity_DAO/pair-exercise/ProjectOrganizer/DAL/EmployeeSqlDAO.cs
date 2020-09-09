using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOrganizer.DAL
{
    public class EmployeeSqlDAO : IEmployeeDAO
    {
        private string connectionString;
        private string sql_getAllEmployees = "SELECT * FROM employee";
        private string sql_getEmployeesWithoutProjects = "SELECT * FROM employee e LEFT OUTER JOIN project_employee pe ON e.employee_id = pe.employee_id WHERE project_id IS NULL"; 
        private string sql_searchForEmployee = "SELECT * FROM employee WHERE first_name = @first_name OR last_name = @last_name"; //CHANGE


        // Single Parameter Constructor
        public EmployeeSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns a list of all of the employees.
        /// </summary>
        /// <returns>A list of all employees.</returns>
        public IList<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql_getAllEmployees, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    while (reader.Read() == true)
                    {
                        int employee_id = Convert.ToInt32(reader["employee_id"]);
                        int department_id = Convert.ToInt32(reader["department_id"]);
                        string first_name = Convert.ToString(reader["first_name"]);
                        string last_name = Convert.ToString(reader["last_name"]);
                        string job_title = Convert.ToString(reader["job_title"]);
                        string birth_date = Convert.ToString(reader["birth_date"]);
                        string gender = Convert.ToString(reader["gender"]);
                        string hire_date = Convert.ToString(reader["hire_date"]);

                        Employee employee = new Employee(employee_id, department_id, first_name, last_name, job_title, birth_date, gender, hire_date);
                        employees.Add(employee);
                    }

                }
            }

            catch (Exception ex)
            {
                return employees;
            }

            return employees;
        }

        /// <summary>
        /// Searches the system for an employee by first name or last name.
        /// </summary>
        /// <remarks>The search performed is a wildcard search.</remarks>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <returns>A list of employees that match the search.</returns>
        public IList<Employee> Search(string firstname, string lastname)
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                 
                    SqlCommand cmd = new SqlCommand(sql_searchForEmployee, conn);

                    cmd.Parameters.AddWithValue("@first_name", firstname);
                    cmd.Parameters.AddWithValue("@last_name", lastname);

                    SqlDataReader reader = cmd.ExecuteReader();
                    Console.WriteLine("Test");

                    while (reader.Read() == true)
                    {
                        

                            int employee_id = Convert.ToInt32(reader["employee_id"]);
                            int department_id = Convert.ToInt32(reader["department_id"]);
                            string first_name = Convert.ToString(reader["first_name"]);
                            string last_name = Convert.ToString(reader["last_name"]);
                            string job_title = Convert.ToString(reader["job_title"]);
                            string birth_date = Convert.ToString(reader["birth_date"]);
                            string gender = Convert.ToString(reader["gender"]);
                            string hire_date = Convert.ToString(reader["hire_date"]);

                        Employee employee = new Employee(employee_id, department_id, first_name, last_name, job_title, birth_date, gender, hire_date);
                        employees.Add(employee);
                    }


                }
            }

            catch (Exception ex)
            {
                return employees;
            }


            return employees;
        }

        /// <summary>
        /// Gets a list of employees who are not assigned to any active projects.
        /// </summary>
        /// <returns></returns>
        public IList<Employee> GetEmployeesWithoutProjects()
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql_getEmployeesWithoutProjects, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read() == true)
                    {
                        int employee_id = Convert.ToInt32(reader["employee_id"]);
                        int department_id = Convert.ToInt32(reader["department_id"]);
                        string first_name = Convert.ToString(reader["first_name"]);
                        string last_name = Convert.ToString(reader["last_name"]);
                        string job_title = Convert.ToString(reader["job_title"]);
                        string birth_date = Convert.ToString(reader["birth_date"]);
                        string gender = Convert.ToString(reader["gender"]);
                        string hire_date = Convert.ToString(reader["hire_date"]);

                        Employee employee = new Employee(employee_id, department_id, first_name, last_name, job_title, birth_date, gender, hire_date);
                        employees.Add(employee);
                    }

                }
            }


            catch (Exception ex)
            {
                return employees;
            }

            return employees;
        }
    }
}
