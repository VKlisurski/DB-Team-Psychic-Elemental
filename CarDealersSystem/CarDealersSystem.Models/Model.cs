namespace CarDealersSystem.Models
{
    using System;

    public class Model
    {
        public Model()
        {
        }

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
