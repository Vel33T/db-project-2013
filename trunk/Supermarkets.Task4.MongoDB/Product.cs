using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Supermarkets.Task4.MongoDB
{
    public class Product
    {
        [BsonId]
        public ObjectId ProductId { get; set; }

        public string ProductName { get; set; }
        public string VendorName { get; set; }
        public int TotalQuantitySold { get; set; }
        public decimal TotalIncomes { get; set; }

        [BsonConstructor]
        public Product(ObjectId productId, string productName, string vendorName, int totalQuantitySold, int totalIncomes)
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.VendorName = vendorName;
            this.TotalQuantitySold = totalQuantitySold;
            this.TotalIncomes = totalIncomes;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
