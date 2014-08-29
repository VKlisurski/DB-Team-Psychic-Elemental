using CarDealersSystem.Models;
using System.Data.Entity;

namespace CarDealersSystem.Data
{
    public class CarDealersSystemDbContext : DbContext
    {
        public CarDealersSystemDbContext() : base("CarDealersSystemConnection")
        {
        }

        public IDbSet<Car> Cars { get; set; }

        public IDbSet<Dealer> Dealers { get; set; }

        public IDbSet<Make> Makes { get; set; }

        public IDbSet<Model> Models { get; set; }

        public IDbSet<Type> Types { get; set; }

        public IDbSet<SalesReport> SalesReports { get; set; }
    }
}