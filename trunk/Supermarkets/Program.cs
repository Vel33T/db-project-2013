using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Data.Entity;
using Supermarkets.Data;
using Supermarkets.Model;

using MySqlSupermarket = Supermarkets.Task1.MySqlSupermarket;

namespace Supermarkets
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sqlserver = new SupermarketsEntities())
            {
                sqlserver.Database.CreateIfNotExists();

                MySqlTransfer.Transfer(sqlserver);
                ExcelTransfer.Transfer(sqlserver);

            }

        }

    }

}
