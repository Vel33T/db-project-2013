using System;
using System.Linq;
using System.Xml;
using MongoDB.Bson;
using MongoDB.Driver;
using Supermarkets.Data;
using Supermarkets.Model;

namespace Supermarkets.Task5.VendorExpencesXML
{
    public class GenerateVendorExpenses
    {
        public static void WriteVendorExpensesReport(SupermarketsEntities sqlserver, string filename)
        {
            using (XmlReader reader = XmlReader.Create(filename))
            {
                string currentVendor = string.Empty;

                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "sale"))
                    {
                        currentVendor = reader.GetAttribute("vendor");

                        if (sqlserver.Vendors.Any(v => v.Name == currentVendor))
                        {
                            Vendor vendor = new Vendor();
                            vendor.Name = currentVendor;
                            sqlserver.Vendors.Add(vendor);
                        }
                    }
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "expenses"))
                    {
                        Vendor vendor = sqlserver.Vendors.Where(v => v.Name == currentVendor).FirstOrDefault();

                        if (vendor != null)
                        {
                            DateTime monthDate = DateTime.Parse(string.Format("01-{0}", reader.GetAttribute("month")));

                            VendorExpenses expense = new VendorExpenses();
                            expense.VendorId = vendor.Id;
                            expense.Month = monthDate.Month;
                            expense.Year = monthDate.Year;
                            expense.Expenses = reader.ReadElementContentAsDecimal();

                            sqlserver.VendorExpenses.Add(expense);

                            AddToMongoDB(expense);
                        }
                    }
                }

                sqlserver.SaveChanges();
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
                { "month", string.Format("{0}-{1}", expense.Month, expense.Year) },
                { "expenses", expense.Expenses.ToString() },
            };

            vendorExpenses.Insert(vendorExpense);
        }

        static void Main()
        {
            using (var context = new SupermarketsEntities())
            {
                WriteVendorExpensesReport(context, @"..\..\VendorExpenses.xml");
            }
        }
    }
}