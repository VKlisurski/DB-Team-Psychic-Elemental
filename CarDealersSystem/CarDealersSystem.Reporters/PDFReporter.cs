namespace CarDealersSystem.Reporters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    using iTextSharp.text;

    using CarDealersSystem.Data;
    using CarDealersSystem.Reporters.Builders;

    public static class PDFReporter
    {
        public static void Export()
        {
            var carDealerDb = new CarDealersSystemDbContext();
            var strBuilder = new StringBuilder();

            var dates = carDealerDb.SalesReports.Select(x => x.ReportDate).ToArray();
            decimal grandTotal = 0m;

            strBuilder.Append("<table border='1'>");
            strBuilder.Append("<tr>");
            strBuilder.Append("<th style=\"font-size:16px; text-align:center;\" colspan='5'>Aggregated Sales Report</th>");
            strBuilder.Append("</tr>");
            strBuilder.Append("</table>");

            for (int i = 0; i < dates.Count() / 3; i++)
            {
                strBuilder.Append("<table border='1'>");
                strBuilder.Append("<tr bgcolor='#BBBBBB'>");
                strBuilder.AppendFormat("<th colspan='5'>Date: {0}</th>", dates[i]);
                strBuilder.Append("</tr>");
                strBuilder.Append("<tr bgcolor='#BBBBBB'>");
                strBuilder.Append("<th class=\"th\"><b>Car Model Name</b></th>");
                strBuilder.Append("<th><b>Quantity</b></th>");
                strBuilder.Append("<th><b>Car Price</b></th>");
                strBuilder.Append("<th><b>Dealer</b></th>");
                strBuilder.Append("<th><b>Sum</b></th>");
                strBuilder.Append("</tr>");

                var cars = carDealerDb.Cars;
                var sales = carDealerDb.SalesReports;

                var reports = cars.Join(sales, car => car.CarID, sale => sale.CarID,
                                          (car, sale) => new
                                          {
                                              CarModel = car.ModelName,
                                              Price = car.Price,
                                              DealerName = sale.DealerName,
                                              Quantity = sale.Quantity,
                                              Sum = sale.Sum * sale.Quantity
                                          });

                decimal totalSum = 0;
                foreach (var sale in reports)
                {
                    totalSum = totalSum + sale.Sum;
                    strBuilder.Append("<tr>");
                    strBuilder.AppendFormat("<td>{0}</td>", sale.CarModel);
                    strBuilder.AppendFormat("<td>{0}</td>", sale.Quantity);
                    strBuilder.AppendFormat("<td>{0}</td>", sale.Price);
                    strBuilder.AppendFormat("<td>{0}</td>", sale.DealerName);
                    strBuilder.AppendFormat("<td>{0}</td>", sale.Sum);
                    strBuilder.Append("</tr>");
                }

                grandTotal = grandTotal + totalSum;

                strBuilder.Append("<tr>");
                strBuilder.AppendFormat("<td colspan='4' align=right>Total Sum for {0}: </td>", dates[i]);
                strBuilder.AppendFormat("<td>{0}</td>", totalSum);
                strBuilder.Append("</tr>");
                strBuilder.Append("</table>");

                if (i != 2)
                {
                    strBuilder.Append("<br />");
                }
            }

            strBuilder.Append("<table border='1'>");
            strBuilder.Append("<tr>");
            strBuilder.Append("<td colspan='4' align=right>Grand total: </td>");
            strBuilder.AppendFormat("<td>{0}</td>", grandTotal);
            strBuilder.Append("</tr>");
            strBuilder.Append("</table>");

            PDFBuilder.HtmlToPdfBuilder builder = new PDFBuilder.HtmlToPdfBuilder(PageSize.LETTER);
            PDFBuilder.HtmlPdfPage page = builder.AddPage();
            page.AppendHtml(strBuilder.ToString());

            byte[] file = builder.RenderPdf();
            string tempFolder = "../../../PDFReports/";
            string tempFileName = DateTime.Now.ToString("yyyy-MM-dd") + "-" + Guid.NewGuid() + ".pdf";
            if (DirectoryExist(tempFolder))
            {
                if (!File.Exists(tempFolder + tempFileName))
                    File.WriteAllBytes(tempFolder + tempFileName, file);
            }
        }
        public static bool DirectoryExist(string directoryPatch)
        {
            DirectoryInfo objDirectory = new DirectoryInfo(directoryPatch);
            if (objDirectory.Exists)
            {
                return true;
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(directoryPatch);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
