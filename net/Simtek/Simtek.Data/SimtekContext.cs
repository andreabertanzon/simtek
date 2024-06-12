using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Simtek.Data;

public partial class SimtekContext : DbContext
{
    public SimtekContext()
    {
    }

    public SimtekContext(DbContextOptions<SimtekContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customer_pk");

            entity.ToTable("customer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Creationdate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creationdate");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .HasColumnName("phone");
            entity.Property(e => e.Stored)
                .HasDefaultValue(false)
                .HasColumnName("stored");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .HasColumnName("surname");
            entity.Property(e => e.Vat)
                .HasMaxLength(100)
                .HasColumnName("vat");
            entity.Property(e => e.Zip)
                .HasMaxLength(20)
                .HasColumnName("zip");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
