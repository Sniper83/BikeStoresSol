using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BikeStoresProj
{
    public partial class BikeStoresContext : DbContext
    {
        public BikeStoresContext()
        {
        }

        public BikeStoresContext(DbContextOptions<BikeStoresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Brands> Brands { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<OrderItems> OrderItems { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Staffs> Staffs { get; set; }
        public virtual DbSet<Stocks> Stocks { get; set; }
        public virtual DbSet<Stores> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BikeStores;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brands>(entity =>
            {
                entity.HasKey(e => e.BrandId)
                    .HasName("PK__brands__5E5A8E279C7831AE");

                entity.ToTable("brands", "production");

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.Property(e => e.BrandName)
                    .IsRequired()
                    .HasColumnName("brand_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__categori__D54EE9B461D3B1FE");

                entity.ToTable("categories", "production");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnName("category_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__customer__CD65CB8527B31FBF");

                entity.ToTable("customers", "sales");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .HasColumnName("street")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasColumnName("zip_code")
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderItems>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ItemId })
                    .HasName("PK__order_it__837942D46D21561E");

                entity.ToTable("order_items", "sales");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasColumnType("decimal(4, 2)");

                entity.Property(e => e.ListPrice)
                    .HasColumnName("list_price")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__order_ite__order__398D8EEE");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__order_ite__produ__3A81B327");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__orders__4659622973BBEAC4");

                entity.ToTable("orders", "sales");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.OrderDate)
                    .HasColumnName("order_date")
                    .HasColumnType("date");

                entity.Property(e => e.OrderStatus).HasColumnName("order_status");

                entity.Property(e => e.RequiredDate)
                    .HasColumnName("required_date")
                    .HasColumnType("date");

                entity.Property(e => e.ShippedDate)
                    .HasColumnName("shipped_date")
                    .HasColumnType("date");

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__orders__customer__33D4B598");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__orders__staff_id__35BCFE0A");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__orders__store_id__34C8D9D1");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__products__47027DF58664E7D9");

                entity.ToTable("products", "production");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.ListPrice)
                    .HasColumnName("list_price")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ModelYear).HasColumnName("model_year");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("product_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK__products__brand___286302EC");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__products__catego__276EDEB3");
            });

            modelBuilder.Entity<Staffs>(entity =>
            {
                entity.HasKey(e => e.StaffId)
                    .HasName("PK__staffs__1963DD9CF71ACE31");

                entity.ToTable("staffs", "sales");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__staffs__AB6E6164CB260F20")
                    .IsUnique();

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ManagerId).HasColumnName("manager_id");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.InverseManager)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK__staffs__manager___30F848ED");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Staffs)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__staffs__store_id__300424B4");
            });

            modelBuilder.Entity<Stocks>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.ProductId })
                    .HasName("PK__stocks__E68284D32E2D100F");

                entity.ToTable("stocks", "production");

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__stocks__product___3E52440B");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__stocks__store_id__3D5E1FD2");
            });

            modelBuilder.Entity<Stores>(entity =>
            {
                entity.HasKey(e => e.StoreId)
                    .HasName("PK__stores__A2F2A30C33324F01");

                entity.ToTable("stores", "sales");

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasColumnName("store_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .HasColumnName("street")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasColumnName("zip_code")
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
