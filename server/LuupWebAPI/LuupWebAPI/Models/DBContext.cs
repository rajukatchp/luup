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
        public DbSet<Actions> Actions { get; set; }
        public DbSet<Conditions> Conditions { get; set; }
        
    }    
}
