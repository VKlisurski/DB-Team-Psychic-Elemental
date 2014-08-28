using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealersSystem.Models
{
    public class Model
    {
        public Model() { }

        public Model(string name, DateTime manufacturingDate, string typeName)
        {
            this.Name = name;
            this.ManufacturingDate = manufacturingDate;
            this.TypeName = typeName;
        }

        public int ModelID { get; set; }

        public string Name { get; set; }

        public DateTime ManufacturingDate { get; set; }

        public string TypeName { get; set; }

        public override string ToString()
        {
            return this.Name + " " + this.ManufacturingDate + " " + this.TypeName;
        }
    }
}
