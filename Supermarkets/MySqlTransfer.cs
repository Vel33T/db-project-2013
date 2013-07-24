using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using Supermarkets.Data;
using Supermarkets.Model;
using MySqlSupermarket = Supermarkets.Task1.MySql.MySqlSupermarket;

namespace Supermarkets
{
    class MySqlTransfer
    {
        static public void Transfer(SupermarketsEntities sqlserver)
        {
            using (var mysql = new MySqlSupermarket())
            {
                // SET IDENTITY_INSERT (Transact-SQL) http://msdn.microsoft.com/en-us/library/ms188059.aspx
                sqlserver.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Vendors ON");
                sqlserver.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Measures ON");
                sqlserver.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Products ON");

                var mysqlTables = new IEnumerable[] { mysql.Vendors, mysql.Measures, mysql.Products };
                var sqlserverEntityFactories = new Func<object>[] { () => new Vendor(), () => new Measure(), () => new Product() };
                var sqlserverTables = new DbSet[] { sqlserver.Vendors, sqlserver.Measures, sqlserver.Products };

                for (int ii = 0; ii < mysqlTables.Length; ii++)
                {
                    foreach (var mysqlObject in mysqlTables[ii])
                    {
                        var sqlserverObject = sqlserverEntityFactories[ii]();
                        sqlserverObject.LoadPropertiesFrom(mysqlObject);
                        sqlserverTables[ii].Add(sqlserverObject);
                    }
                }

                sqlserver.SaveChanges();

                sqlserver.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Vendors OFF");
                sqlserver.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Measures OFF");
                sqlserver.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Products OFF");
            }
        }
    }
}