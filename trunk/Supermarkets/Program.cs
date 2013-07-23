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
            }

            MySqlTransfer.Transfer();
        }
    }

    static class Utils
    {
        // http://stackoverflow.com/questions/6961248/copy-properties-between-objects-using-reflection-and-extesnion-method
        public static void LoadPropertiesFrom(this object target, object source)
        {
            PropertyInfo[] sourceProps = source.GetType().GetProperties();
            var targetType = target.GetType();

            foreach (PropertyInfo sourceProp in sourceProps)
            {
                var targetProperty = targetType.GetProperty(sourceProp.Name);

                if (targetProperty == null) continue;
                if (!targetProperty.CanWrite) continue;

                object sourceValue = sourceProp.GetValue(source, null);

                if (sourceValue == null)
                    continue;

                targetProperty.SetValue(target, sourceValue, null);
            }
        }


        struct ColumnInfo
        {
            public double WidthWeight { get; set; }
        }

        public static int GetMinimumWidth()
        {
            return 0;
        }
    }
}
