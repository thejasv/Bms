using Microsoft.EntityFrameworkCore;
namespace BMS.Models
{
    public class AccountContext:DbContext
    {
        public AccountContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Account> Accounts { get; set; }
            
    }
}
