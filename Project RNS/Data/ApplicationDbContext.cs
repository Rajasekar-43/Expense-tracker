using Microsoft.EntityFrameworkCore;
using Project_RNS.Models;

namespace Project_RNS.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 

        }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Login> SignUps { get; set; }

    }
}
