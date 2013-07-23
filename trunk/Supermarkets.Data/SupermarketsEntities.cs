using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarkets.Model;


namespace Supermarkets.Data
{
    public class SupermarketsEntities : DbContext
    {
        public bool Transfer { get; private set; }

        public SupermarketsEntities(bool transfer = false)
        {
            this.Transfer = transfer;
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var error in ex.EntityValidationErrors)
                {
                    Console.WriteLine(error.Entry);
                    foreach (var verror in error.ValidationErrors)
                        Console.WriteLine(verror);
                }

                throw;
            }

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<ProductSupermarketSale> Sales { get; set; }
        public DbSet<Supermarket> Supermarkets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // http://stackoverflow.com/questions/5142431/eft-ctp5-disable-identity-column-on-insert
            if (!Transfer)
            {
                modelBuilder.Entity<Vendor>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                modelBuilder.Entity<Product>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                modelBuilder.Entity<Measure>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
