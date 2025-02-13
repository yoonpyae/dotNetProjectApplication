using Microsoft.EntityFrameworkCore;

namespace dotNetProject.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Township> Townships { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _ = optionsBuilder.UseSqlServer("Server=.;Database=DotNetProject;User ID=sa;Password=123;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<Township>(entity =>
        {
            _ = entity.ToTable("Township");

            _ = entity.Property(e => e.TownshipId)
                .HasMaxLength(50)
                .HasColumnName("TownshipID");
            _ = entity.Property(e => e.TownshipName).HasMaxLength(50);
        });

        _ = modelBuilder.Entity<User>(entity =>
        {
            _ = entity.ToTable("User");

            _ = entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("UserID");
            _ = entity.Property(e => e.Address)
                .HasMaxLength(10)
                .IsFixedLength();
            _ = entity.Property(e => e.UserName)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
