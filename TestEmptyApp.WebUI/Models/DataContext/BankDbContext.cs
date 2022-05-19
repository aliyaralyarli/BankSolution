using Microsoft.EntityFrameworkCore;
using TestEmptyApp.WebUI.Models.Entities;

namespace TestEmptyApp.WebUI.Models.DataContext
{
    public class BankDbContext:DbContext
    {
        public BankDbContext(DbContextOptions options)
            :base(options)
        {

        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> user { get; set; }
    }
}
