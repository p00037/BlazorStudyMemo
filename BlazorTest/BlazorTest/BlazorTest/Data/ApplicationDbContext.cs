using BlazorTest.Domain.Tenant;
using BlazorTest.Domain.Weather;
using BlazorTest.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorTest.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IUserService _userService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserService userService)
            : base(options)
        {
            _userService = userService;
        }

        public DbSet<WeatherForecastEntity> WeatherForecasts => Set<WeatherForecastEntity>();
        public DbSet<TenantEntity> Tenants  => Set<TenantEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<TenantEntity>().ToTable("Tenants").Property(c => c.Id).IsRequired();
            modelBuilder.Entity<WeatherForecastEntity>().ToTable("WeatherForecasts").Property(c => c.Id).IsRequired();

            // グローバルクエリフィルタ：TenantId が現在のテナントに一致するもののみ
            modelBuilder.Entity<WeatherForecastEntity>().HasQueryFilter(c => c.TenantId == int.Parse(_userService.GetTenantId()));
        }
    }
}
