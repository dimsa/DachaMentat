using Microsoft.EntityFrameworkCore;

namespace DachaMentat.Db
{
    public class MentatUsersDbContext : DbContext
    {
    //    private string _connectionString;
        private DbInitDelegate _dbInit;

        public DbSet<User> Users { get; set; }        

        public MentatUsersDbContext(DbInitDelegate  dbInit)
        {
            //_connectionString = connectionString;
            _dbInit = dbInit;    
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new DbConfiguration());

            // Configure the primary key for the Sensor entity
            modelBuilder.Entity<User>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<User>()
                .Property(e => e.Id)
                .UseMySqlIdentityColumn()
                .ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _dbInit(optionsBuilder);
        }
    }
}
