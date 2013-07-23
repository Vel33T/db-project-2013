using System;
using System.Xml;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarkets.Data;
using Supermarkets.Model;

namespace Supermarkets.Task5.VendorExpencesXML
{
    class GenerateVendorExpenses
    {
        static void Main()
        {
            using (XmlReader reader = XmlReader.Create(@"..\..\VendorExpenses.xml"))
            {
                using (SupermarketsEntities context = new SupermarketsEntities())
                {
                    string currentVendor = string.Empty;

                    while (reader.Read())
                    {

                        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "sale"))
                        {
                            currentVendor = reader.GetAttribute("vendor");

                            if (context.Vendors.Any(v => v.Name == currentVendor))
                            {
                                Vendor vendor = new Vendor();
                                vendor.Name = currentVendor;
                                context.Vendors.Add(vendor);
                            }
                        }
                        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "expenses"))
                        {
                            Vendor vendor = context.Vendors.Where(v => v.Name == currentVendor).FirstOrDefault();

                            if (vendor != null)
                            {
                                DateTime monthDate = DateTime.Parse("01-" + reader.GetAttribute("month"));

                                VendorExpenses expense = new VendorExpenses();
                                expense.VendorId = vendor.Id;
                                expense.Month = monthDate.Month;
                                expense.Year = monthDate.Year;
                                expense.Expenses = reader.ReadElementContentAsDecimal();

                                context.VendorExpenses.Add(expense);
                            }
                        }
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
