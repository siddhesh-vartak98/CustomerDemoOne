using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CustomerDemoOne.Models;

public partial class CustomerDemoOneDbContext : DbContext
{
    public CustomerDemoOneDbContext()
    {
    }

    public CustomerDemoOneDbContext(DbContextOptions<CustomerDemoOneDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustomerMaster> CustomerMasters { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=CustomerDemoOneDB;User Id=sa;Password=sid@123456;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerMaster>(entity =>
        {
            entity.HasKey(e => e.CustomerId);

            entity.ToTable("CustomerMaster");

            entity.Property(e => e.CustomerId).HasColumnName("customerID");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.EmailId)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("emailID");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("mobileNumber");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.WhenEntered)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime")
                .HasColumnName("whenEntered");
            entity.Property(e => e.WhenModified)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime")
                .HasColumnName("whenModified");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
