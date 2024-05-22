using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication16.Models;

namespace WebApplication16.DAL
{
    public class AppDbContext:IdentityDbContext<User>
    {
       public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Portfolio> Portfolio { get; set; }
    }
}
