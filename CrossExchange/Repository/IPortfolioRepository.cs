﻿using System.Linq;

namespace CrossExchange
{
    public interface IPortfolioRepository : IGenericRepository<Portfolio>
    {
        IQueryable<Portfolio> GetAll();

        bool IsExists(int portfolioId);
    }
}