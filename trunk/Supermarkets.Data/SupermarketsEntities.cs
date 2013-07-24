using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using Supermarkets.Model;

namespace Supermarkets.Data
{
    public class SupermarketsEntities : DbContext
    {
        public SupermarketsEntities(bool transfer = false)
        {
            this.Transfer = transfer;
        }

        public bool Transfer { get; private set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<VendorExpenses> VendorExpenses { get; set; }

        public DbSet<Measure> Measures { get; set; }

        public DbSet<ProductSupermarketSale> Sales { get; set; }

        public DbSet<Supermarket> Supermarkets { get; set; }

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
                    {
                        Console.WriteLine(verror);
                    }
                }

                throw;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // http://stackoverflow.com/questions/5142431/eft-ctp5-disable-identity-column-on-insert
            if (!this.Transfer)
            {
                modelBuilder.Entity<Vendor>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                modelBuilder.Entity<Product>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                modelBuilder.Entity<Measure>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}