using ICBC.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICBC.DAL.Context
{
    public class ICBCContextSQLite : DbContext
    {
        public DbSet<Trade> Trades { get; set; }

        public string DbPath { get; }

        public ICBCContextSQLite()
        {
            DbPath = @"C:\ICBCTradeData.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trade>(entity =>
            {
                entity.ToTable("Trade");

                entity.Property(e => e.TradeId).ValueGeneratedNever();
                entity.Property(e => e.BusinessUnit).HasColumnType("varchar(3)");
                entity.Property(e => e.Instrument).HasColumnType("varchar(10)");
                entity.Property(e => e.ProfitCentre).HasColumnType("INT");
                entity.Property(e => e.ReportingAmount).HasColumnType("INT");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        private void OnModelCreatingPartial(ModelBuilder modelBuilder) { }
    }
}