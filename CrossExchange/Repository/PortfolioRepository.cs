using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CrossExchange
{
    public class PortfolioRepository : GenericRepository<Portfolio>, IPortfolioRepository
    {
        public PortfolioRepository(ExchangeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Portfolio> GetAll()
        {
            return _dbContext.Portfolios.Include(x => x.Trade).AsQueryable();
        }

        public bool IsExists(int portfolioId)
        {
          var isExists =  _dbContext.Portfolios.Any(x => x.Id.Equals(portfolioId));
          return isExists;
        }
    }
}