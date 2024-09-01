using PSW24.Core.Domain;
using PSW24.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Infrastructure.Database.Repositories
{
    public class ProblemRepository : IProblemRepository
    {
        private readonly Context _dbContext;
        public ProblemRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public Problem Create(Problem problem)
        {
            _dbContext.Problems.Add(problem);
            _dbContext.SaveChanges();
            return problem;
        }
    }
}
