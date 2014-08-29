using CarDealersSystem.Data;
using CarDealersSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealersSystem.MongoDbSynchronizator;
using ZipExcelExtractor;


namespace CarDealersSystem.ConsoleClient
{
    class ConsoleClient
    {
        static void Main(string[] args)
        {
            Synchronizator.Run();

            Extractor ext = new Extractor("..\\..\\");
            ext.ExtractFromArchive("Sales-Reports.zip");
        }
    }
}
