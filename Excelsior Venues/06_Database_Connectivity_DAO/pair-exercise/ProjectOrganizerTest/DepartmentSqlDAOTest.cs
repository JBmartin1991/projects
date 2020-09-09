using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectOrganizer;
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
    public class DepartmentSqlDAOTest
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

                string sql_insert = "INSERT INTO department (name) VALUES ('TestDepartment');";
                SqlCommand cmd = new SqlCommand(sql_insert, conn);
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            transaction.Dispose();
        }

        [TestMethod]
        public void TestGetDepartment()
        {
            DepartmentSqlDAO department = new DepartmentSqlDAO(connectionString);
            IList<Department> departmentList = department.GetDepartments();

            bool found = false;
            foreach(Department item in departmentList)
            {
                if(item.Name == "TestDepartment")
                {
                    found = true;
                    break;
                }
            }

            Assert.IsTrue(found);
        }
    

        [TestMethod] // CreateDepartment();
        public void TestCreateDepartment()
        {
            DepartmentSqlDAO department = new DepartmentSqlDAO(connectionString);
            Department newDepartment = new Department("NewDepartment");
            int result = department.CreateDepartment(newDepartment);

            bool success = false;

            if(result > 0)
            {
                success = true;
            }

            Assert.IsTrue(success);
        }

        [TestMethod] // UpdateDepartment();
        public void TestUpdateDepartment()
        {
            DepartmentSqlDAO department = new DepartmentSqlDAO(connectionString);
            Department updateDepartment = new Department(1, "UpdateDepartment");
            bool result = department.UpdateDepartment(updateDepartment);

            
            Assert.IsTrue(result);
        }

    }
}
