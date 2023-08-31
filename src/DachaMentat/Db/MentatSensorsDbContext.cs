using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DachaMentat.Db
{
    public class MentatSensorsDbContext : DbContext
    {
        public DbSet<Sensor> Sensors { get; set; }

        public DbSet<Indication> Indications { get; set; }

        public MentatSensorsDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            optionsBuilder.UseSqlite("Data Source=mentat.db");
        }
    }
}
