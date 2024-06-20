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

    public virtual DbSet<Intervention> Interventions { get; set; }

    public virtual DbSet<InterventionWorker> InterventionWorkers { get; set; }

    public virtual DbSet<Site> Sites { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=simtek;Username=postgres;Password=test");

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

        modelBuilder.Entity<Intervention>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("intervention_pk");

            entity.ToTable("intervention");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_update_date");
            entity.Property(e => e.Material)
                .HasColumnType("json")
                .HasColumnName("material");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.SiteId).HasColumnName("site_id");
            entity.Property(e => e.Stored)
                .HasDefaultValue(false)
                .HasColumnName("stored");

            entity.HasOne(d => d.Site).WithMany(p => p.Interventions)
                .HasForeignKey(d => d.SiteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("intervention_site_id_fk");
        });

        modelBuilder.Entity<InterventionWorker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("intervention_worker_pk");

            entity.ToTable("intervention_worker");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HourSpent).HasColumnName("hour_spent");
            entity.Property(e => e.InterventionId).HasColumnName("intervention_id");
            entity.Property(e => e.WorkerId).HasColumnName("worker_id");

            entity.HasOne(d => d.Intervention).WithMany(p => p.InterventionWorkers)
                .HasForeignKey(d => d.InterventionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("intervention_worker_intervention_id_fk");

            entity.HasOne(d => d.Worker).WithMany(p => p.InterventionWorkers)
                .HasForeignKey(d => d.WorkerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("intervention_worker_worker_id_fk");
        });

        modelBuilder.Entity<Site>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_id");

            entity.ToTable("site");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.LastUpdateDat).HasColumnName("last_update_dat");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Stored)
                .HasDefaultValue(false)
                .HasColumnName("stored");
            entity.Property(e => e.Zip)
                .HasMaxLength(30)
                .HasColumnName("zip");

            entity.HasOne(d => d.Customer).WithMany(p => p.Sites)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("site_customer_id_fk");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("worker_pk");

            entity.ToTable("worker");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PricePerHour).HasColumnName("price_per_hour");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .HasColumnName("surname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
