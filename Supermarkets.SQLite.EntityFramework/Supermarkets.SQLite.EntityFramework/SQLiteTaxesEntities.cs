using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarkets.SQLite.EntityFramework
{
    public class SQLiteTaxesEntities : DbContext
    {
        public SQLiteTaxesEntities() : base("SQLiteTaxesEntities") { }

        public DbSet<ProductTax> ProductTaxes { get; set; }

        public DbSet<VendorFinancialResult> VendorFinancialResults { get; set; }

    }
}
