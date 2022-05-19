using Microsoft.EntityFrameworkCore;

namespace ApiBank.Models
{
    public class BankDbContext:DbContext
    {
        public BankDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; }
    }
}
