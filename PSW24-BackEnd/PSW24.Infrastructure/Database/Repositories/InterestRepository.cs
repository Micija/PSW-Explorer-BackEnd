using PSW24.Core.Domain;
using PSW24.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Infrastructure.Database.Repositories
{
    public class InterestRepository : IInterestRepository
    {
        private readonly Context _dbContext;

        public InterestRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Interest> GetAll()
        {
            return _dbContext.Interests.ToList();
        }
    }
}
