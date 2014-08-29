namespace CarDealersSystem.Models
{
    public class Make
    {
        public Make()
        {
        }

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
