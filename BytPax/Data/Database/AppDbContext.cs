using Microsoft.EntityFrameworkCore;
using BytPax.Models;
using BytPax.Models.core;

namespace BytPax.Data.Database;
public class AppDbContext : DbContext
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<Athlete> Athletes { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<HistoricalEvent> HistoricalEvents { get; set; }
    public DbSet<RecordHistory> RecordHistories { get; set; }
    public DbSet<Sport> Sports { get; set; }


    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasDiscriminator<string>("RoleType")
            .HasValue<AdminUser>("Admin")
            .HasValue<RegularUser>("Visitor");

        modelBuilder.Entity<Article>().ToTable("Articles");
        modelBuilder.Entity<Athlete>().ToTable("Athletes");
        modelBuilder.Entity<Category>().ToTable("Categories");
        modelBuilder.Entity<HistoricalEvent>().ToTable("HistoricalEvents");
        modelBuilder.Entity<RecordHistory>().ToTable("RecordHistories");
        modelBuilder.Entity<Sport>().ToTable("Sports");
        modelBuilder.Entity<User>().ToTable("Users");

        // Конфігурація зв’язків
        modelBuilder.Entity<Article>()
            .HasOne<Category>()
            .WithMany(c => c.Articles)
            .HasForeignKey(a => a.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Athlete>()
            .HasOne(a => a.Category)
            .WithMany(c => c.Athletes)
            .HasForeignKey(a => a.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HistoricalEvent>()
            .HasOne<Sport>()
            .WithMany(s => s.HistoricalEvents)
            .HasForeignKey(h => h.SportId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HistoricalEvent>()
            .HasOne<Category>()
            .WithMany()
            .HasForeignKey(h => h.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }

}