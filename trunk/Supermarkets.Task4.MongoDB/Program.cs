using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Supermarkets.Task4.MongoDB
{
    public class Program
    {
        static void Main()
        {
            var connectionStr = @"mongodb://dev:1234@ds063297.mongolab.com:63287/db-project-product-reports";
            var client = new MongoClient(connectionStr);
            var server = client.GetServer();
            var db = server.GetDatabase("db-project-product-reportst");
            MongoCollection<BsonDocument> products = db.GetCollection<BsonDocument>("products");
            BsonDocument product = new BsonDocument 
            {
                { "product-id", "3" },
                { "product-name", "Beer “Beck’s”" },
                { "vendor-name", "Zagorka Corp." },
                { "total-quantity-sold", "673" },
                { "total-incomes", "609.24" }
            };
            products.Insert(product);
        }
    }
}