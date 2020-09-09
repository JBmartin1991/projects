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
    public class EmployeeSqlDAOTest
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=EmployeeDB;Integrated Security=True";
        private TransactionScope transaction;

        [TestInitialize]
        public void Initialize()
        {
            transaction = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql_insert = "INSERT INTO employee (department_id, first_name, last_name, job_title, birth_date, gender, hire_date)  VALUES (1, 'Matt', 'Eland', 'TRAITOR', '1982/10/16', 'M', '2019/11/11');";
                SqlCommand cmd = new SqlCommand(sql_insert, conn);
                cmd.ExecuteNonQuery();

                string sql_insert2 = "INSERT INTO employee (department_id, first_name, last_name, job_title, birth_date, gender, hire_date)  VALUES (1, 'JB', 'Martin', 'Cool Guy', '1991/03/12', 'M', '2019/11/11');";
                SqlCommand cmd2 = new SqlCommand(sql_insert2, conn);
                cmd2.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            transaction.Dispose();
        }

        [TestMethod]
        public void TestGetAllEmployees()
        {
            EmployeeSqlDAO employee = new EmployeeSqlDAO(connectionString);
            IList<Employee> employeeList = employee.GetAllEmployees();

            bool found = false;
            foreach (Employee person in employeeList)
            {
                if (person.JobTitle == "TRAITOR")
                {
                    found = true;
                    break;
                }
            }

            Assert.IsTrue(found);
        }

        [TestMethod]
        public void TestSearch()
        {
            EmployeeSqlDAO employee = new EmployeeSqlDAO(connectionString);
            IList<Employee> employeeSearchList = employee.Search("JB", "Martin");

            bool result = false;

           foreach(Employee person in employeeSearchList)
            {
                if (person.FirstName == "JB")
                {
                    result = true;
                    break;
                }
            }

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestGetEmployeesWithoutProjects()
        {
            EmployeeSqlDAO employee = new EmployeeSqlDAO(connectionString);
            IList<Employee> employeeWithoutProjectList = employee.GetEmployeesWithoutProjects();

            int count = 0;

            foreach(Employee person in employeeWithoutProjectList)
            {
                if (person.FirstName == "JB")
                {
                    count++;
                }

                if (person.JobTitle == "TRAITOR")
                {
                    count++;
                }
            }

            Assert.AreEqual(2, count);
        }
    }
}
