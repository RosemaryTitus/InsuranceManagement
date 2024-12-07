using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InsuranceManagement.Models;

public partial class InsuranceManagementDatabaseContext : DbContext
{
    public InsuranceManagementDatabaseContext()
    {
    }

    public InsuranceManagementDatabaseContext(DbContextOptions<InsuranceManagementDatabaseContext> options)
        : base(options)
    {
    }

    

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<PolicyType> PolicyTypes { get; set; }

    public virtual DbSet<Policy> Policies { get; set; }

    public virtual DbSet<Rule> Rules { get; set; }


    public virtual DbSet<Purchase> Purchases { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=InsuranceManagementDatabase;Trusted_Connection =True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07B6995466");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534764CF3E5").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(220)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PolicyType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Policy_T__3214EC07498FC62A");

            entity.ToTable("Policy_Type");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.EffectiveFrom).HasColumnType("datetime");
            entity.Property(e => e.EffectiveTo).HasColumnType("datetime");
            entity.Property(e => e.LastUpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Rgid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("RGID");
            entity.Property(e => e.TypeName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Policy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Policy__3214EC0721133F4C");

            entity.ToTable("Policy");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EffectiveFrom).HasColumnType("datetime");
            entity.Property(e => e.EffectiveTo).HasColumnType("datetime");
            entity.Property(e => e.LastUpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.PolicyNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PolicyType)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PolicyTerm)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PremiumAmount).HasColumnType("decimal(30, 2)");
            entity.Property(e => e.Rgid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("RGID");
        });

        modelBuilder.Entity<Rule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rules__3214EC07AEA18621");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ActionType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ActionValue).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ConditionOperator)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ConditionType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ConditionValue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.PolicyType)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Rgid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("RGID");
        });


        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Purchase__3214EC077DAD0AC7");

            entity.ToTable("Purchase");

            entity.HasIndex(e => e.PaymentId, "UQ__Purchase__9B556A39DBA08A80").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.PolicyType)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PurchaseDate).HasColumnType("datetime");
            entity.Property(e => e.Rgid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("RGID");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.TotalPremiumAmount).HasColumnType("decimal(30, 2)");
        });


        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
