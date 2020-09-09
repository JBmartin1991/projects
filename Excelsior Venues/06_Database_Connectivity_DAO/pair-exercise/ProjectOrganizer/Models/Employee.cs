using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOrganizer.Models
{
    public class Employee
    {
        private int employee_id;
        private int department_id;
        private string first_name;
        private string last_name;
        private string job_title;
        private string birth_date;
        private char gender;
        private string hire_date;

        public Employee(int employee_id, int department_id, string first_name, string last_name, string job_title, string birth_date, string gender, string hire_date)
        {
            EmployeeId = employee_id;
            DepartmentId = department_id;
            FirstName = first_name;
            LastName = last_name;
            JobTitle = job_title;
            BirthDate = birth_date;
            Gender = gender;
            HireDate = hire_date;
        }

        /// <summary>
        /// The employee id.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// The department id the employee works for.
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// The job title the employee has.
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        /// The first name of the employee.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the employee.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The employee's birth date.
        /// </summary>
        public string BirthDate { get; set; }

        /// <summary>
        /// The employee's gender.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// The employee's hire date.
        /// </summary>
        public string HireDate { get; set; }

        
    }
}
