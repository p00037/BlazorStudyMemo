using BlazorTest.Domain.WeatherForecast;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorTest.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<WeatherForecastEntity> WeatherForecasts => Set<WeatherForecastEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WeatherForecastEntity>().ToTable("WeatherForecasts").Property(c => c.Id).IsRequired();
        }
    }
}
