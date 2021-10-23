using Microsoft.EntityFrameworkCore;
using Weather.Entities;

namespace Weather.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<WeatherEntity> Weathers { get; set; }
        public DbSet<WeatherHistory> WeatherHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherEntity>()
               .HasMany(p => p.WeatherHistory)
               .WithOne(b => b.WeatherEntity)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
        }
    }
}
