using CrudMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudMvc.Database.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {

        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Immobile> Immobiles { get; set; }
        
    }
}
