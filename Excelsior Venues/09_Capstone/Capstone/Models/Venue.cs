using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
   public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string State { get; set; }

      


        public Venue(int id, string name, string description, string city, string state)
        {
            Id = id;
            Name = name;
            Description = description;
            City = city;
            State = state;
           

        }

        public Venue()
        {
        }



        //public override string ToString()
        //{
        //    return Id.ToString() + Name + Description + City + State + Categories;

        //}

    }
}
