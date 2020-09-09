using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOrganizer.DAL
{
    public class ProjectSqlDAO : IProjectDAO
    {
        private string connectionString;
        private string sql_getAllProjects = "SELECT * FROM project";
        private string sql_assignEmployeeToProject = "INSERT INTO project_employee (project_id, employee_id) "
            + "VALUES (@project_id, @employee_id)";
        private string sql_removeEmployeeFromProject = "DELETE FROM project_employee WHERE project_id = @project_id AND employee_id = @employee_id";
        private string sql_createProject = "INSERT INTO project (name, from_date, to_date) " + 
            "VALUES (@name, @from_date, @to_date)";

        // Single Parameter Constructor
        public ProjectSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns all projects.
        /// </summary>
        /// <returns></returns>
        public IList<Project> GetAllProjects()
        {
            List<Project> projects = new List<Project>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql_getAllProjects, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read() == true)
                    {
                        int project_id = Convert.ToInt32(reader["project_id"]);
                        string name = Convert.ToString(reader["name"]);
                        DateTime from_date = Convert.ToDateTime(reader["from_date"]);
                        DateTime to_date = Convert.ToDateTime(reader["to_date"]);


                        Project project = new Project(project_id, name, from_date, to_date);
                        projects.Add(project);

                    }
                }
            }

            catch (Exception ex)
            {
                return projects;
            }

            return projects;
        
        }

        /// <summary>
        /// Assigns an employee to a project using their IDs.
        /// </summary>
        /// <param name="projectId">The project's id.</param>
        /// <param name="employeeId">The employee's id.</param>
        /// <returns>If it was successful.</returns>
        public bool AssignEmployeeToProject(int projectId, int employeeId)
        {
            List<Project> projects = new List<Project>();
            bool result = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql_assignEmployeeToProject, conn);
                    cmd.Parameters.AddWithValue("@project_id", projectId);
                    cmd.Parameters.AddWithValue("@employee_id", employeeId);
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

        /// <summary>
        /// Removes an employee from a project.
        /// </summary>
        /// <param name="projectId">The project's id.</param>
        /// <param name="employeeId">The employee's id.</param>
        /// <returns>If it was successful.</returns>
        public bool RemoveEmployeeFromProject(int projectId, int employeeId)
        {
            List<Project> projects = new List<Project>();
            bool result = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql_removeEmployeeFromProject, conn);
                    cmd.Parameters.AddWithValue("@project_id", projectId);
                    cmd.Parameters.AddWithValue("@employee_id", employeeId);
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

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="newProject">The new project object.</param>
        /// <returns>The new id of the project.</returns>
        public int CreateProject(Project newProject)
        {
            int result = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql_createProject, conn);

                    cmd.Parameters.AddWithValue("@name", newProject.Name);
                    cmd.Parameters.AddWithValue("@from_date", newProject.StartDate);
                    cmd.Parameters.AddWithValue("@to_date", newProject.EndDate);

                    cmd = new SqlCommand("SELECT Max(project_id) FROM project;", conn);
                    result = Convert.ToInt32(cmd.ExecuteScalar());

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
