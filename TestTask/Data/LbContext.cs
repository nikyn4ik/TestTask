using Microsoft.EntityFrameworkCore;

namespace TestTask.Data
{
    public class LbContext : DbContext
    {
        public LbContext(DbContextOptions<LbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}