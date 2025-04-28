using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Traveler.Models.Entities;

namespace Traveler.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Stay> Stays { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0786FE356C");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053421F02504").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        modelBuilder.Entity<Stay>()
            .HasOne(e => e.User)
            .WithMany(u => u.Stays)
            .HasForeignKey(e => e.UserId);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
