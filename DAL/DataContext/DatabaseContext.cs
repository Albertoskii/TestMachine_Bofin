using DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace DAL.DataContext
{
    public class DatabaseContext : DbContext
    {
        public class OptionsBuild
        {

            public OptionsBuild()
            {
                settings = new AppConfiguration();
                opsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
                opsBuilder.UseSqlServer(settings.sqlConnectionString);
                dbOptions = opsBuilder.Options;
            }

            public DbContextOptionsBuilder<DatabaseContext> opsBuilder { get; set; }
            public DbContextOptions<DatabaseContext> dbOptions { get; set; } //get DatabaseInformation
            private AppConfiguration settings { get; set; }   //get ConnString

        }
        public static OptionsBuild ops = new OptionsBuild();

        //references to entities, specify 
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options){}

        public DbSet<OrderDetails> OrderDetails{ get; set; }
     
    }
  
}
