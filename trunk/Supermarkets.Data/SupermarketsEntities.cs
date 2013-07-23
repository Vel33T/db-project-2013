using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarkets.Model;


namespace Supermarkets.Data
{
    public class SupermarketsEntities : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<ProductSupermarketSale> Sales { get; set; }
        public DbSet<Supermarket> Supermarkets { get; set; }
    }
}
