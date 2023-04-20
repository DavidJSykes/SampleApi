using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VpDataAccess.Models;

namespace VpDataAccess.Data
{
    public class VpDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public VpDbContext(DbContextOptions<VpDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("VpDatabase");
                optionsBuilder.UseSqlite(connectionString);
            }
        }
    }
}
