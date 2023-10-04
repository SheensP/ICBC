using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ICBC.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Metadata;
using System.Configuration;

namespace ICBC.DAL.Context
{
    public class ICBCContext : DbContext
    {
        public DbSet<Trade> Trades { get; set; }

        public string _dbPath { get; }

        public ICBCContext(string connectionString)
        {
            _dbPath = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(_dbPath);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trade>(entity =>
            {
                entity.ToTable("Trade");

                entity.Property(e => e.TradeId);
                entity.Property(e => e.BusinessUnit);
                entity.Property(e => e.Instrument);
                entity.Property(e => e.ProfitCentre);
                entity.Property(e => e.ReportingAmount);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        private void OnModelCreatingPartial(ModelBuilder modelBuilder) { }
    }
}