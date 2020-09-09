using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOrganizer.Models
{
    public class Department
    {

        public Department(string name)
        {
           
            Name = name;
        }

        public Department(int department_id, string name)
        {
            Id = department_id;
            Name = name;
        }

        /// <summary>
        /// The dept id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The dept name.
        /// </summary>
        public string Name { get; set; }
    }
}
