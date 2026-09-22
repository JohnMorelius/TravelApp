 using Microsoft.EntityFrameworkCore;
using TravelApp.Models.Extensions;
using TravelApp.Models.DTO;

namespace TravelApp.Models;

public class TravelAppDbContext : DbContext
{
    public TravelAppDbContext() { }
    public TravelAppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Land> Land => Set<Land>();
    public DbSet<Ort> Ort => Set<Ort>();
    public DbSet<Sevardhet> Sevardhet => Set<Sevardhet>();
    public DbSet<Kommentar> Kommentar => Set<Kommentar>();
    public DbSet<Anvandare> Anvandare => Set<Anvandare>();

    public DbSet<DbInfoDto> DbInfoView => Set<DbInfoDto>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder = optionsBuilder.ConfigureForDesignTime(
                (options, connectionString) => options.UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure()));
        }

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Ort>()
            .HasOne(o => o.Land).WithMany(l => l.Orter)
            .HasForeignKey(o => o.LandId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Sevardhet>()
            .HasOne(s => s.Ort).WithMany(o => o.Sevardheter)
            .HasForeignKey(s => s.OrtId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Kommentar>()
            .HasOne(k => k.Sevardhet).WithMany(s => s.Kommentarer)
            .HasForeignKey(k => k.SevardhetId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Kommentar>()
            .HasOne(k => k.Anvandare).WithMany(a => a.Kommentarer)
            .HasForeignKey(k => k.AnvandareId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Anvandare>().HasIndex(a => a.Epost).IsUnique();
        modelBuilder.Entity<Sevardhet>().HasIndex(s => s.Kategori);

        modelBuilder.Entity<DbInfoDto>().ToView("vwDbInfo").HasNoKey();
    }
}