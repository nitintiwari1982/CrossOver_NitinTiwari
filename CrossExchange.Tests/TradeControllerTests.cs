using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using CrossExchange.Controller;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Extensions.Configuration;

namespace CrossExchange.Tests
{
    [TestFixture]
   public class TradeControllerTests : TestBase
    {
           
        public TradeModel TradeModel { get; set; }
        private TradeController tradeController;
        
        [SetUp]
        public void SetUpTradeController()
        {
            base.SetUp();                     
            TradeModel = new TradeModel();
            tradeController = new TradeController(ShareRepository, TradeRepository, PortfolioRepository);
        }

        [Test]
        [TestCase("BUY", 10, 1, "ABC")]// Symbol doesn't exists
        [TestCase("SELL", 1000, 1, "REL")] // Portfolio have insufficent to sell
        [TestCase("BUY", 10, 5, "REL")]// Portfolio doesn't exists
        [TestCase("SEL", 10, 5, "REL")]// No SEL operation Exists
        [TestCase("BUYY", 10, 5, "REL")]// No BUYY Operatin exists
        public async Task Get_bad_request_for_all_mismatches(string action, int quantity, int portfolioId, string symbol)
        {
            TradeModel.Action = action;
            TradeModel.NoOfShares = quantity;
            TradeModel.PortfolioId = portfolioId;
            TradeModel.Symbol = symbol;
            var result = await tradeController.Post(TradeModel);
            Assert.NotNull(result);
            var createResult = result as BadRequestResult;
            Assert.AreEqual(400, createResult.StatusCode);
        }

        [Test]
        [TestCase("BUY", 10, 1, "REL")]
        [TestCase("SELL", 10, 1, "REL")]
        public async Task Buy_share_symbol_exists(string action, int quantity, int portfolioId, string symbol)
        {
            TradeModel.Action = action;
            TradeModel.NoOfShares = quantity;
            TradeModel.PortfolioId = portfolioId;
            TradeModel.Symbol = symbol;

            var result = await tradeController.Post(TradeModel);
            Assert.NotNull(result);
            var createResult = result as CreatedResult;
            Assert.AreEqual(201, createResult.StatusCode);
        }
    }
}
