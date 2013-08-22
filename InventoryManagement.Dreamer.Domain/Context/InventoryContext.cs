using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using InventoryManagement.Dreamer.Domain.Migrations;
using InventoryManagement.Dreamer.Entitys;

namespace InventoryManagement.Dreamer.Domain.Context
{
    public class InventoryContext : DbContext
    {
        public InventoryContext()
            : base("name=InventoryAssociate")
        {

        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sku> DbSet { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sku>()
                .HasRequired(x=>x.Product)
                .WithMany(x=>x.Skuses)
                .HasForeignKey(x=>x.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasRequired(x=>x.Brand)
                .WithMany(x=>x.Productses)
                .HasForeignKey(x=>x.BrandId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Brand>()
                .HasRequired(x=>x.Company)
                .WithMany(x=>x.Brandses)
                .HasForeignKey(x=>x.CompanyId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TransactionHistory>()
                .HasRequired(x => x.Transaction)
                .WithMany(x => x.TransactionHistories)
                .HasForeignKey(x => x.TransctionId)
                .WillCascadeOnDelete(false);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<InventoryContext, Configuration>());
            base.OnModelCreating(modelBuilder);
        }
    }
}
