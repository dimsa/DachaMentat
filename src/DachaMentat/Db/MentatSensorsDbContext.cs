﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace DachaMentat.Db
{
    public class MentatSensorsDbContext : DbContext
    {
        private DbInitDelegate _dbInit;

        public DbSet<Sensor> Sensors { get; set; }

        public DbSet<Indication> Indications { get; set; }

        public MentatSensorsDbContext(DbInitDelegate dbInit) : base()
        {
            _dbInit = dbInit;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new DbConfiguration());

            // Configure the primary key for the Sensor entity
            modelBuilder.Entity<Sensor>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Sensor>()
                .Property(e => e.Id)
                .UseMySqlIdentityColumn()
                .ValueGeneratedOnAdd();

            // Configure the primary key for the Indication entity
           modelBuilder.Entity<Indication>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Indication>()
                .Property(e => e.Id)
                .UseMySqlIdentityColumn()
                .ValueGeneratedOnAdd();

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
            _dbInit(optionsBuilder);           
        }
    }
}
