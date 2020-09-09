using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOrganizer.Models
{
    public class Project
    {
        private string from_date;
        private string to_date;

        public Project(int project_id, string name, DateTime from_date, DateTime to_date)
        {
            ProjectId = project_id;
            Name = name;
            StartDate = from_date;
            EndDate = to_date;
        }

        public Project(string name, DateTime from_date, DateTime to_date)
        {
          
            Name = name;
            StartDate = from_date;
            EndDate = to_date;
        }

        /// <summary>
        /// The project's id.
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// The project's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The project's start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The project's end date.
        /// </summary>
        public DateTime EndDate { get; set; }        
    }
}
