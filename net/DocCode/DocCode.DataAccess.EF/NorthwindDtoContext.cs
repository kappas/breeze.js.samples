using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Northwind.DtoModels
{
    internal class NorthwindDtoContext : DbContext
    {
        static NorthwindDtoContext()
        {
            // Prevent attempt to initialize a database for this context
            Database.SetInitializer<NorthwindDtoContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerDtoConfiguration());
            modelBuilder.Configurations.Add(new OrderDtoConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailDtoConfiguration());
            modelBuilder.Configurations.Add(new ProductDtoConfiguration());
            modelBuilder.Configurations.Add(new SupplierDtoConfiguration());
        }

        //  Dto versions of these Northwind Model classes
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}

#region EntityType Configurations

namespace Northwind.DtoModels
{
    using System.Data.Entity.ModelConfiguration;

    internal class CustomerDtoConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerDtoConfiguration()
        {
            Property(c => c.CustomerID)
              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(c => c.CompanyName).IsRequired().HasMaxLength(40);
        }
    }

    internal class OrderDtoConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderDtoConfiguration()
        {
            Property(o => o.CustomerName).HasMaxLength(40);
        }
    }

    internal class OrderDetailDtoConfiguration : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailDtoConfiguration()
        {
            HasKey(od => new { od.OrderID, od.ProductID });
            Property(od => od.OrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(od => od.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }

    internal class ProductDtoConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductDtoConfiguration()
        {
            Property(p => p.ProductName).IsRequired().HasMaxLength(40);       
        }
    }

    internal class SupplierDtoConfiguration : EntityTypeConfiguration<Supplier>
    {
        public SupplierDtoConfiguration() {
            Property(s => s.CompanyName).IsRequired().HasMaxLength(40);
        }
    }
}

#endregion
