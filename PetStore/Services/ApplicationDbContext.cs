using Microsoft.EntityFrameworkCore;
using PetStore.Models;

namespace PetStore.Services
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext (options)
    {
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        //{
            
        //}

        public DbSet<Product> Products { get; set; }
    }
}