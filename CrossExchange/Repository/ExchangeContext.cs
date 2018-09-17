using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossExchange
{
    public class ExchangeContext : DbContext
    {
        public ExchangeContext()
        {
        }

        public ExchangeContext(DbContextOptions<ExchangeContext> options) : base(options)
        {
        }


        public DbSet<Portfolio> Portfolios { get; set; }

        public DbSet<Trade> Trades { get; set; }

        public DbSet<HourlyShareRate> Shares { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Portfolio>().HasData(new Portfolio { Id = 1 , Name = "John Doe"});

            modelBuilder.Entity<Trade>().HasData(
            new  { Id = 1, NoOfShares = 50, Action = "BUY", Price = 5000.0M, Symbol = "REL" , PortfolioId = 1 },
            new  { Id = 2, NoOfShares = 100, Action = "BUY", Price = 10000.0M, Symbol = "REL", PortfolioId = 1 },
            new  { Id = 3, NoOfShares = 150, Action = "BUY", Price = 14250.0M, Symbol = "CBI", PortfolioId = 1 },
            new  { Id = 4, NoOfShares = 70, Action = "SELL", Price = 6790.0M, Symbol = "CBI", PortfolioId = 1 });

            modelBuilder.Entity<HourlyShareRate>().HasData(
                new HourlyShareRate { Id = 1, Symbol = "REL", Rate = 90, TimeStamp = new DateTime(2018, 08, 13, 01, 00, 00) },
                new HourlyShareRate { Id = 2, Symbol = "REL", Rate = 95, TimeStamp = new DateTime(2018, 08, 13, 02, 00, 00) },
                new HourlyShareRate { Id = 3, Symbol = "REL", Rate = 100, TimeStamp = new DateTime(2018, 08, 13, 03, 00, 00) },
                new HourlyShareRate { Id = 4, Symbol = "REL", Rate = 89, TimeStamp = new DateTime(2018, 08, 13, 04, 00, 00) },
                new HourlyShareRate { Id = 5, Symbol = "REL", Rate = 110, TimeStamp = new DateTime(2018, 08, 13, 05, 00, 00) },
                new HourlyShareRate { Id = 6, Symbol = "REL", Rate = 96, TimeStamp = new DateTime(2018, 08, 13, 06, 00, 00) },
                new HourlyShareRate { Id = 7, Symbol = "REL", Rate = 97, TimeStamp = new DateTime(2018, 08, 13, 07, 00, 00) },
                new HourlyShareRate { Id = 8, Symbol = "REL", Rate = 99, TimeStamp = new DateTime(2018, 08, 13, 08, 00, 00) },
                new HourlyShareRate { Id = 9, Symbol = "CBI", Rate = 91, TimeStamp = new DateTime(2018, 08, 13, 01, 00, 00) },
                new HourlyShareRate { Id = 10, Symbol = "CBI", Rate = 96, TimeStamp = new DateTime(2018, 08, 13, 02, 00, 00) },
                new HourlyShareRate { Id = 11, Symbol = "CBI", Rate = 105, TimeStamp = new DateTime(2018, 08, 13, 03, 00, 00) },
                new HourlyShareRate { Id = 12, Symbol = "CBI", Rate = 87, TimeStamp = new DateTime(2018, 08, 13, 04, 00, 00) },
                new HourlyShareRate { Id = 13, Symbol = "CBI", Rate = 100, TimeStamp = new DateTime(2018, 08, 13, 05, 00, 00) },
                new HourlyShareRate { Id = 14, Symbol = "CBI", Rate = 98, TimeStamp = new DateTime(2018, 08, 13, 06, 00, 00) },
                new HourlyShareRate { Id = 15, Symbol = "CBI", Rate = 95, TimeStamp = new DateTime(2018, 08, 13, 07, 00, 00) },
                new HourlyShareRate { Id = 16, Symbol = "CBI", Rate = 92, TimeStamp = new DateTime(2018, 08, 13, 08, 00, 00) });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
