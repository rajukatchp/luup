using Microsoft.EntityFrameworkCore;

namespace LuupWebAPI.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Workflow> Workflow { get; set; }
        public DbSet<Template> Template { get; set; }
    }    
}
