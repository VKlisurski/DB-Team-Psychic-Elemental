namespace CarDealersSystem.Models
{
    public class Type
    {
        public Type()
        {
        }

        public Type(string name)
        {
            this.Name = name;
        }

        public int TypeID { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
