using PSW24.Core.Domain;
using PSW24.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Infrastructure.Database.Repositories
{
    public class KeyPointRepository : IKeyPointRepository
    {
        private readonly Context _dbContext;
        public KeyPointRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public KeyPoint Create(KeyPoint keyPoint)
        {
            _dbContext.KeyPoints.Add(keyPoint);
            _dbContext.SaveChanges();
            return keyPoint;
        }
    }
}
