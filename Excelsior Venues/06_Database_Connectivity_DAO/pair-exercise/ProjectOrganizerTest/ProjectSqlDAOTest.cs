using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectOrganizer.DAL;
using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;

namespace ProjectOrganizerTest
{
    [TestClass]
    public class ProjectSqlDAOTest
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=EmployeeDB;Integrated Security=True";
        private TransactionScope transaction;
        int newProjectId = 0;

        [TestInitialize]
        public void Initialize()
        {
            transaction = new TransactionScope();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql_insert = "INSERT INTO project (name, from_date, to_date) VALUES ('ProjectDoesNotMatter', '2025/12/12', '2050/12/11');";
                SqlCommand cmd = new SqlCommand(sql_insert, conn);
                cmd.ExecuteNonQuery();

                string sql_getProjectId = "SELECT MAX(project_id) FROM project;";
                SqlCommand getProjectId = new SqlCommand(sql_getProjectId, conn);
                newProjectId = (int)getProjectId.ExecuteScalar();

                string sql_insert2 = "INSERT INTO employee (department_id, first_name, last_name, job_title, birth_date, gender, hire_date)  VALUES (1, 'Matt', 'Eland', 'TRAITOR', '1982/10/16', 'M', '2019/11/11');";
                SqlCommand cmd2 = new SqlCommand(sql_insert2, conn);
                cmd2.ExecuteNonQuery();

                string sql_insert3 = "INSERT INTO employee (department_id, first_name, last_name, job_title, birth_date, gender, hire_date)  VALUES (1, 'JB', 'Martin', 'Cool Guy', '1991/03/12', 'M', '2019/11/11');";
                SqlCommand cmd3 = new SqlCommand(sql_insert3, conn);
                cmd3.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            transaction.Dispose();
        }

        [TestMethod]
        public void GetAllProjects()
        {
            ProjectSqlDAO project = new ProjectSqlDAO(connectionString);
            IList<Project> projectList = project.GetAllProjects();

            bool found = false;
            foreach (Project item in projectList)
            {
                if (item.Name == "ProjectDoesNotMatter")
                {
                    found = true;
                    break;
                }
            }

            Assert.IsTrue(found);
        }

        [TestMethod]
        public void TestAssignEmployeeToProject()
        {
            ProjectSqlDAO project = new ProjectSqlDAO(connectionString);
            //Project deathStarProject = new Project("DeathStarQualityControl", DateTime.Now, DateTime.Now);
            //project.CreateProject(deathStarProject);
            //bool result = project.AssignEmployeeToProject(8, 14);
            bool result2 = project.AssignEmployeeToProject(newProjectId, 4);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql_select = "SELECT * FROM project_employee WHERE employee_id = 4 AND project_id = @newProjectId;";

                SqlCommand cmd = new SqlCommand(sql_select, conn);
                cmd.Parameters.AddWithValue("@newProjectId", newProjectId);

                SqlDataReader reader = cmd.ExecuteReader();

                int count = 0;

                while (reader.Read())
                {
                    count++;
                }

                Assert.AreEqual(1, count);
            }

        }

        [TestMethod]
        public void TestCreateProject()
        {
            ProjectSqlDAO project = new ProjectSqlDAO(connectionString);
            Project newProject = new Project("DeathStarQualityControl", DateTime.Now, DateTime.Now);
            int result = project.CreateProject(newProject);

            bool success = false;

            if (result > 0)
            {
                success = true;
            }

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void TestRemoveEmployeeFromProject()
        {
            ProjectSqlDAO project = new ProjectSqlDAO(connectionString);
            //Project deathStarProject = new Project("DeathStarQualityControl", DateTime.Now, DateTime.Now);
            //project.CreateProject(deathStarProject);
            //bool result = project.AssignEmployeeToProject(8, 14);
            bool result2 = project.RemoveEmployeeFromProject(1, 2);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql_select = "SELECT * FROM project_employee WHERE employee_id = 2 AND project_id = 1;";

                SqlCommand cmd = new SqlCommand(sql_select, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                int count = 0;

                while (reader.Read())
                {
                    count++;
                }

                Assert.AreEqual(0, count);
            }
        }
    }
}
