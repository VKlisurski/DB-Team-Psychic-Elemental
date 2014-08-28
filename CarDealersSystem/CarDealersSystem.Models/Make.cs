using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealersSystem.Models
{
    public class Make
    {
        public Make() { }

        public Make(string name)
        {
            this.Name = name;
        }

        public int MakeID { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
