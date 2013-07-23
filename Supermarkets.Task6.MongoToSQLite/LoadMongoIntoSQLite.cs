using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using MongoDB;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using Supermarkets.SQLite.EntityFramework;

namespace Supermarkets.Task6.MongoToSQLite
{
    public class LoadMongoIntoSQLite
    {
        public static void Load(SQLiteTaxesEntities sqlite)
        {
            var connectionStr = @"mongodb://dev:1234@ds063297.mongolab.com:63287/db-project-product-reports";
            var client = new MongoClient(connectionStr);
            var server = client.GetServer();
            var db = server.GetDatabase("db-project-product-reports");

            MongoCollection<BsonDocument> products = db.GetCollection<BsonDocument>("products");
            MongoCollection<BsonDocument> vendorExpenses = db.GetCollection<BsonDocument>("vendor-expenses");


            // fixme: toDictionary?
            var expenses = vendorExpenses.AsQueryable();

            var byVendor = products.AsQueryable().GroupBy(p => p["vendor-name"].ToString());

            foreach (var gr in byVendor)
            {
                var expenseStr = expenses.Where(e => e["vendor-name"] == gr.Key).First()["expenses"].ToString();
                var vfr = new VendorFinancialResult
                {
                    VendorName = gr.Key,
                    Expenses = decimal.Parse(expenseStr),
                };

                var totalIncome = 0.0m;
                var tax = 0.0m;
                foreach (var productData in gr)
                {
                    var income = decimal.Parse(productData["total-incomes"].ToString());

                    var taxPercentage = sqlite.ProductTaxes
                        .Where(p => p.ProductName == productData["product-name"].ToString())
                        .First().Tax;

                    var taxAmount = income * taxPercentage / 100;

                    totalIncome += income;
                    taxPercentage += taxAmount;

                }

                vfr.Income = totalIncome;
                vfr.Tax = tax;
                vfr.FinancialResult = totalIncome - tax - vfr.Expenses;
                sqlite.VendorFinancialResults.Add(vfr);
            }

            /*     BsonDocument vendorExpense = new BsonDocument
             {
                 { "vendor-name", expense.Vendor.Name },
                 { "month", expense.Month + "-" + expense.Year },
                 { "expenses", expense.Expenses.ToString() },
             };
         */
            /*
             *                         { "product-id", item.ProductId },
                        { "product-name", item.ProductName },
                        { "vendor-name", item.VendorName },
                        { "total-quantity-sold", item.TotalQuantitySold.ToString() },
                        { "total-incomes", item.TotalIncomes.ToString() }
             */


        }
    }
}
