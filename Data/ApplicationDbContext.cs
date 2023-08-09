using GadgetFreaks.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GadgetFreaks.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Gadget> Gadgets { get; set; }

        public DbSet<Category> Categories { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}