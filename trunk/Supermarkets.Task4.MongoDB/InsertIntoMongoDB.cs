using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Supermarkets.Data;
using Supermarkets.Model;
using MongoDB.Driver.Builders;

namespace Supermarkets.Task4.MongoDB
{
    public class InsertIntoMongoDB
    {
        static void Main()
        {
            GenerateMongoDBProductReport();
        }

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
                            group sale
                            by new
                            {
                                ProductId = sale.ProductId,
                                ProductName = sale.Product.Name,
                                VendorName = sale.Product.Vendor.Name
                            } into g
                            select new
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
                }
            }
        }
    }
}