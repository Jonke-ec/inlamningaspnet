using Microsoft.EntityFrameworkCore;
using WebApi.Models.Entities;

namespace WebApi {
    public class SqlContext : DbContext {

        public SqlContext() {

        }

        public SqlContext(DbContextOptions<SqlContext> options) : base(options) {

        }

        public DbSet<AddressEntity> Addresses { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }

        public DbSet<OrderEntity> Orders { get; set; }
    }
}
