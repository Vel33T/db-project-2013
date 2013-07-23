using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarkets.SQLite.EntityFramework
{
    class SQLiteTaxesEntities : DbContext
    {
        public DbSet<ProductTax> ProductTaxes;

    }
}
