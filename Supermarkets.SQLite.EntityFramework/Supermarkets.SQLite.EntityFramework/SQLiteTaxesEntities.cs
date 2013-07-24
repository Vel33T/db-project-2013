using System;
using System.Data.Entity;
using System.Linq;

namespace Supermarkets.SQLite.EntityFramework
{
    public class SQLiteTaxesEntities : DbContext
    {
        public SQLiteTaxesEntities() : base("SQLiteTaxesEntities")
        {
        }

        public DbSet<ProductTax> ProductTaxes { get; set; }

        public DbSet<VendorFinancialResult> VendorFinancialResults { get; set; }
    }
}