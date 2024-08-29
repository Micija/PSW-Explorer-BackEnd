using PSW24.BuildingBlocks.Infrastructure.Database;
using PSW24.Core.Domain;
using PSW24.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Infrastructure.Database.Repositories
{
    public class InterestRepository : CrudDatabaseRepository<Tour, Context>, IInterestRepository
    {
        private readonly Context _dbContext;

        public InterestRepository(Context dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Interest> GetAll()
        {
            return _dbContext.Interests.ToList();
        }

        public Interest GetByType(string type)
        {
            return _dbContext.Interests.FirstOrDefault(x => x.Type == type);
        }
    }
}
