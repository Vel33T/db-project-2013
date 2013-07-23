using System;
using System.Xml;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarkets.Task5.VendorExpencesXML
{
    class VendorExpenses
    {
        static void Main()
        {
            using (XmlReader reader = XmlReader.Create(@"..\..\VendorExpenses.xml"))
            { 
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "sale"))
                    {
                        Console.WriteLine(reader.);
                    }
                }
            }
        }
    }
}
