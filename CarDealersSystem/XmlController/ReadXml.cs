using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XmlController
{
    public static class ReadXml
    {
        public static List<Tuple<string, string, string>> GetObjects(string fileName)
        {
            List<Tuple<string, string, string>>
            looses = new List<Tuple<string, string, string>>();

            XDocument xmlDoc = XDocument.Load(fileName);

            foreach (var dealer in xmlDoc.Descendants("dealer"))
            {
                string dealerName = dealer.Attribute("dealerName").Value;

                foreach (var loose in dealer.Descendants("loose"))
                {
                    string month = loose.Attribute("month").Value;
                    string value = loose.Value;

                    looses.Add(new Tuple<string, string, string>(dealerName, month, value));
                }
            }

            return looses;
        }
    }
}
