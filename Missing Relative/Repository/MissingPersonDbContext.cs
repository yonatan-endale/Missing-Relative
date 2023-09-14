using Microsoft.EntityFrameworkCore;
using Missing_Relative.Models;

namespace Missing_Relative.Repository
{
    public class MissingPersonDbContext:DbContext
    {
       public MissingPersonDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server = .; database = MissingPersonDb; 
            integrated security = true; TrustServerCertificate = True;");
        }
        public DbSet<User> Users { get; set; }


    }
}
