using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ISAuto.Model;

public partial class DatabaseAutoContext : DbContext
{
    public DatabaseAutoContext()
    {
    }

    public DatabaseAutoContext(DbContextOptions<DatabaseAutoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AutoPart> AutoParts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<PartsInOeder> PartsInOeders { get; set; }

    public virtual DbSet<PartsInStore> PartsInStores { get; set; }

    public virtual DbSet<PartsInSupply> PartsInSupplies { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Supply> Supplies { get; set; }

    public virtual DbSet<TypePart> TypeParts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ILYA_PC\\SQLEXPRESS;Database=DatabaseAuto;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AutoPart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CarParts");

            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Image)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PurchasePrise).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TypeId).HasColumnName("TypeID");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.AutoParts)
                .HasForeignKey(d => d.ManufacturerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CarParts_Manufacturers");

            entity.HasOne(d => d.Type).WithMany(p => p.AutoParts)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AutoParts_TypeParts");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Patronymic)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PositionId).HasColumnName("PositionID");

            entity.HasOne(d => d.Position).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Position");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Order");

            entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.DateTimePurchase).HasColumnType("datetime");
        });

        modelBuilder.Entity<PartsInOeder>(entity =>
        {
            entity.ToTable("PartsInOeder");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.PartsInStoreId).HasColumnName("PartsInStoreID");

            entity.HasOne(d => d.Order).WithMany(p => p.PartsInOeders)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PartsInOeder_Orders");

            entity.HasOne(d => d.PartsInStore).WithMany(p => p.PartsInOeders)
                .HasForeignKey(d => d.PartsInStoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PartsInOeder_PartsInStore");
        });

        modelBuilder.Entity<PartsInStore>(entity =>
        {
            entity.ToTable("PartsInStore");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AutoPartId).HasColumnName("AutoPartID");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.AutoPart).WithMany(p => p.PartsInStores)
                .HasForeignKey(d => d.AutoPartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PartsInStore_AutoParts");
        });

        modelBuilder.Entity<PartsInSupply>(entity =>
        {
            entity.ToTable("PartsInSupply");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AutoPartId).HasColumnName("AutoPartID");
            entity.Property(e => e.SupplyId).HasColumnName("SupplyID");

            entity.HasOne(d => d.AutoPart).WithMany(p => p.PartsInSupplies)
                .HasForeignKey(d => d.AutoPartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PartsInSupply_AutoParts");

            entity.HasOne(d => d.Supply).WithMany(p => p.PartsInSupplies)
                .HasForeignKey(d => d.SupplyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PartsInSupply_Supplies");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.ToTable("Position");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Suppliers)
                .HasForeignKey(d => d.ManufacturerId)
                .HasConstraintName("FK_Suppliers_Manufacturers");
        });

        modelBuilder.Entity<Supply>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

            entity.HasOne(d => d.Employee).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Supplies_Employees");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Supplies_Suppliers");
        });

        modelBuilder.Entity<TypePart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CategoryParts");

            entity.Property(e => e.Imade)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
