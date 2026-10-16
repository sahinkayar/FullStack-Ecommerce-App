using api.models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace api.Data
{
    public class ECommerceDbContext : IdentityDbContext<AppUser>
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> dbContextoptions) : base(dbContextoptions)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
       
    }
}
