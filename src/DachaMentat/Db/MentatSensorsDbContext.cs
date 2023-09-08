using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace DachaMentat.Db
{
    public class MentatSensorsDbContext : DbContext
    {
        private string _connectionString;

        public DbSet<Sensor> Sensors { get; set; }

        public DbSet<Indication> Indications { get; set; }

        public MentatSensorsDbContext(string connectionString)
        {
            _connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new DbConfiguration());

            // Configure the primary key for the Sensor entity
            modelBuilder.Entity<Sensor>()
                .HasKey(e => e.Id);

            // Configure the primary key for the Indication entity
            modelBuilder.Entity<Indication>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Sensor>(entity =>
            {
                entity.Property(e => e.UnitOfMeasure).IsRequired();
            });

            modelBuilder.Entity<Indication>(entity =>
            {
                entity.Property(e => e.SensorId).IsRequired();                
            });

    
               base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}
