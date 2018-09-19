using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossExchange.Tests
{
   public class TestBase
    {
        public ITradeRepository TradeRepository { get; set; }
        public IPortfolioRepository PortfolioRepository { get; set; }
        public IShareRepository ShareRepository { get; set; }

        [SetUp]
        public void SetUp()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = config["ConnectionStrings:DefaultConnection"];
            var exchangeContext = new DbContextOptionsBuilder<ExchangeContext>();
            exchangeContext.UseSqlServer(connectionString);
            PortfolioRepository = new PortfolioRepository(new ExchangeContext(exchangeContext.Options));
            TradeRepository = new TradeRepository(new ExchangeContext(exchangeContext.Options));
            ShareRepository = new ShareRepository(new ExchangeContext(exchangeContext.Options));
        }

        [TearDown]
        public void TearDown()
        {
            TradeRepository = null;
            PortfolioRepository = null;
            ShareRepository = null;
        }
    }
}
