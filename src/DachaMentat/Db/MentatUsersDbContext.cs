using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace DachaMentat.Db
{
    public class MentatUsersDbContext : DbContext
    {
        private string _connectionString;

        public DbSet<User> Users { get; set; }        

        public MentatUsersDbContext(string connectionString)
        {
            _connectionString = connectionString;
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
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}
