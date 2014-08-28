using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealersSystem.Models
{
    public class Dealer
    {
        public Dealer() { }

        public Dealer(string name, string country, string city, decimal income = 0)
        {
            this.Name = name;
            this.Country = country;
            this.City = city;
            this.Income = income;
        }

        public int DealerID { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public decimal Income { get; set; }

        public override string ToString()
        {
            return this.Country + " " + this.City + " " + this.Name;
        }
    }
}
