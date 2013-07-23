using System;
using System.Xml;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarkets.Data;
using Supermarkets.Model;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Supermarkets.Task5.VendorExpencesXML
{
    public class GenerateVendorExpenses
    {
        static void Main()
        {
            WriteVendorExpensesReport(@"..\..\VendorExpenses.xml");
        }

        public static void WriteVendorExpensesReport(string filename)
        {

            using (XmlReader reader = XmlReader.Create(filename))
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

                            AddToMongoDB(expense);
                        }
                    }
                }

                context.SaveChanges();
            }
        }

        public static void AddToMongoDB(VendorExpenses expense)
        {
            var connectionStr = @"mongodb://dev:1234@ds063297.mongolab.com:63287/db-project-product-reports";
            var client = new MongoClient(connectionStr);
            var server = client.GetServer();
            var db = server.GetDatabase("db-project-product-reports");
            MongoCollection<BsonDocument> vendorExpenses = db.GetCollection<BsonDocument>("vendor-expenses");

            BsonDocument vendorExpense = new BsonDocument
                {
                    { "vendor-name", expense.Vendor.Name },
                    { "month", expense.Month + "-" + expense.Year },
                    { "expenses", expense.Expenses.ToString() },
                };

            vendorExpenses.Insert(vendorExpense);
        }
    }
}
