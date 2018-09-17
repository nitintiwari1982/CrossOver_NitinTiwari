using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CrossExchange.Controller
{
    [Route("api/Trade")]
    public class TradeController : ControllerBase
    {
        private IShareRepository _shareRepository { get; set; }
        private ITradeRepository _tradeRepository { get; set; }
        private IPortfolioRepository _portfolioRepository { get; set; }

        public TradeController(IShareRepository shareRepository, ITradeRepository tradeRepository, IPortfolioRepository portfolioRepository)
        {
            _shareRepository = shareRepository;
            _tradeRepository = tradeRepository;
            _portfolioRepository = portfolioRepository;
        }


        [HttpGet("{portfolioid}")]
        public async Task<IActionResult> GetAllTradings([FromRoute]int portFolioid)
        {
            var trade = _tradeRepository.Query().Where(x => x.PortfolioId.Equals(portFolioid));
            return Ok(trade);
        }



        /*************************************************************************************************************************************
        For a given portfolio, with all the registered shares you need to do a trade which could be either a BUY or SELL trade. For a particular trade keep following conditions in mind:
		BUY:
        a) The rate at which the shares will be bought will be the latest price in the database.
		b) The share specified should be a registered one otherwise it should be considered a bad request. 
		c) The Portfolio of the user should also be registered otherwise it should be considered a bad request. 
                
        SELL:
        a) The share should be there in the portfolio of the customer.
		b) The Portfolio of the user should be registered otherwise it should be considered a bad request. 
		c) The rate at which the shares will be sold will be the latest price in the database.
        d) The number of shares should be sufficient so that it can be sold. 
        Hint: You need to group the total shares bought and sold of a particular share and see the difference to figure out if there are sufficient quantities available for SELL. 

        *************************************************************************************************************************************/

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TradeModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_portfolioRepository.IsExists(model.PortfolioId))
                {
                  return BadRequest(string.Format("Sorry {0} not a registered portfolio.", model.PortfolioId));
                }
                switch (model.Action)
                {
                    case "BUY":
                        if (!IsSymbolExists(model.Symbol))
                        {
                            var buyShare = await _shareRepository.Query().Where(x => x.Symbol.Equals(model.Symbol)).OrderByDescending(x => x.TimeStamp).FirstOrDefaultAsync();
                            await _tradeRepository.InsertAsync(MapToDb(model, buyShare));
                        }
                        else
                        {
                            return BadRequest(string.Format("Sorry share {0} doesn't asvailable to buy", model.Symbol));
                        }
                       
                        break;
                    case "SELL":
                        var sellShare = await _shareRepository.Query().Where(x => x.Symbol.Equals(model.Symbol)).OrderByDescending(x => x.TimeStamp).FirstOrDefaultAsync();
                        var availableForSell = GetAvailableShareToSell(model);
                        if(availableForSell < model.NoOfShares)
                        {
                            return BadRequest(string.Format("sorry only {0} share available to sell", availableForSell));
                        }
                        else
                        {
                            var sellDetail = MapToDb(model, sellShare);
                            await _tradeRepository.InsertAsync(sellDetail);
                        }
                        
                        break;
                    default:
                        
                        break;
                }
            }
            return Created("Trade", model);
        }

        [NonAction]
        private bool IsSymbolExists(string symbol)
        {
            var isExists = _shareRepository.Query().Where(x => x.Symbol.Equals(symbol)).Any();
            return isExists;
        } 
        
        [NonAction]
        private Trade MapToDb(TradeModel model, HourlyShareRate shareDetail)
        {
            var trade = new Trade();
            trade.PortfolioId = model.PortfolioId;
            trade.Symbol = model.Symbol;
            trade.NoOfShares = model.NoOfShares;
            trade.Price = model.NoOfShares * shareDetail.Rate;
            trade.Action = model.Action;
            return trade;
        }

        private int GetAvailableShareToSell(TradeModel model)
        {
            //// var shares = _tradeRepository.Query().Where(x => x.PortfolioId.Equals(model.PortfolioId) && x.Action.Equals("BUY")).ToList().GroupBy(y => new { y.Symbol, y.NoOfShares });

            var result = from bb in _tradeRepository.Query().Where(x => x.PortfolioId.Equals(model.PortfolioId) && x.Action.Equals("BUY") && x.Symbol.Equals(model.Symbol))
                     group bb by new { bb.Symbol, bb.PortfolioId } into g
                     select new
                     {
                         Symbol = g.Sum(r => r.NoOfShares)
                     };
            return result.First().Symbol;
        }
    }
}
