using System;
using System.IO;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using Supermarkets.Data;

namespace Supermarkets.Task4.MongoDB
{
    public class InsertIntoMongoDB
    {
        public static void GenerateMongoDBProductReport()
        {
            var connectionStr = @"mongodb://dev:1234@ds063297.mongolab.com:63287/db-project-product-reports";
            var client = new MongoClient(connectionStr);
            var server = client.GetServer();
            var db = server.GetDatabase("db-project-product-reports");
            MongoCollection<BsonDocument> products = db.GetCollection<BsonDocument>("products");

            using (var sqlserver = new SupermarketsEntities())
            {
                var query = from sale in sqlserver.Sales
                            group sale by new
                            {
                                ProductId = sale.ProductId,
                                ProductName = sale.Product.Name,
                                VendorName = sale.Product.Vendor.Name
                            }
                            into g select new
                            {
                                ProductId = g.Key.ProductId,
                                ProductName = g.Key.ProductName,
                                VendorName = g.Key.VendorName,
                                TotalQuantitySold = g.Sum(x => x.Quantity),
                                TotalIncomes = g.Sum(y => y.Quantity * y.UnitPrice)
                            };

                foreach (var item in query)
                {
                    BsonDocument product = new BsonDocument 
                    {
                        { "product-id", item.ProductId },
                        { "product-name", item.ProductName },
                        { "vendor-name", item.VendorName },
                        { "total-quantity-sold", item.TotalQuantitySold.ToString() },
                        { "total-incomes", item.TotalIncomes.ToString() }
                    };

                    products.Insert(product);
                    Directory.CreateDirectory("output\\json");
                    File.WriteAllText(string.Format("output\\json\\{0}.json", item.ProductId), product.ToJson());
                }
            }
        }

        static void Main()
        {
            GenerateMongoDBProductReport();
        }
    }
}