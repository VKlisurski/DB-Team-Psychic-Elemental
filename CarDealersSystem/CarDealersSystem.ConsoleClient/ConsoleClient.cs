using CarDealersSystem.Data;
using CarDealersSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealersSystem.MongoDbSynchronizator;


namespace CarDealersSystem.ConsoleClient
{
    class ConsoleClient
    {
        static void Main(string[] args)
        {
            Synchronizator.Run();
            using (var dbContext = new CarDealersSystemDbContext())
            {
                Synchronizator.Run();
            }
        }
    }
}
