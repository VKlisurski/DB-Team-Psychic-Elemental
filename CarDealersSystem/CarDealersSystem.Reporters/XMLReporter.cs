namespace CarDealersSystem.Reporters
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    using CarDealersSystem.Data;
    using CarDealersSystem.Reporters.Contracts;

    public class XMLReporter : IReporter
    {
        public void Report()
        {
            string folderPath = "../../../XMLReports";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string reportFilePath = folderPath + "/Sales-Report-" + DateTime.Now.ToString("yyyy-MM-dd") + ".xml";

            using (var dbContext = new CarDealersSystemDbContext())
            {
                XElement rootElement = new XElement("sales");

                var dealers = dbContext.Dealers.ToList();
                foreach (var dealer in dealers)
                {
                    var entries =
                        from s in dbContext.SalesReports
                        join d in dbContext.Dealers on s.DealerName equals d.Name
                        where d.Name == dealer.Name
                        group s by s.ReportDate into salesByDealer
                        select new
                        {
                            date = salesByDealer.Key,
                            totalSum = salesByDealer.Sum(saleReport => saleReport.Sum)
                        };

                    if (entries.Count() == 0)
                    {
                        continue;
                    }

                    XElement saleElement = new XElement("sale");
                    foreach (var entry in entries)
                    {
                        saleElement.SetAttributeValue("dealer", dealer.Name);
                        saleElement.Add(new XElement("summary",
                            new XAttribute("date", entry.date),
                            new XAttribute("total-sum", entry.totalSum.ToString("0.00"))));
                    }

                    rootElement.Add(saleElement);
                }

                rootElement.Save(reportFilePath);
            }
        }
    }
}