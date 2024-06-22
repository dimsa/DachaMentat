using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

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
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new DbConfiguration());

            // Configure the primary key for the Sensor entity
            modelBuilder.Entity<User>()
                .HasKey(e => e.Id);
 
             base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _dbInit(optionsBuilder);
        }
    }
}
