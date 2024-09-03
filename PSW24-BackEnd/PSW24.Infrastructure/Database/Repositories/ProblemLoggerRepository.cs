using PSW24.Core.Domain;
using PSW24.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Infrastructure.Database.Repositories
{
    public class ProblemLoggerRepository : IProblemLoggerRepository
    {
        private readonly Context _dbContext;
        public ProblemLoggerRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public ProblemLogger Create(ProblemLogger problemLogger)
        {
            _dbContext.ProblemLoggers.Add(problemLogger);
            _dbContext.SaveChanges();
            return problemLogger;
        }
    }
}
