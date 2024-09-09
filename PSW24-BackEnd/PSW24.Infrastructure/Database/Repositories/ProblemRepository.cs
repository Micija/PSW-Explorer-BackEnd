using Microsoft.EntityFrameworkCore;
using PSW24.Core.Domain;
using PSW24.Core.Domain.Enums;
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

        public Problem GetById(long problemId)
        {
            return _dbContext.Problems.Include(p => p.User).Include(p => p.Tour).First(p => p.Id == problemId);
        }

        public List<Problem> GetForAuthor(long authorId)
        {
            return _dbContext.Problems.Include(p => p.Tour).Where(p => p.Tour.AuthorId == authorId).ToList();
        }

        public List<Problem> GetNewForAuthor(long authorId)
        {
            var ret = _dbContext.Problems
                .Include(p => p.Tour)
                .Where(p => p.Tour.AuthorId == authorId && p.Status == ProblemStatus.ON_HOLD)
                .ToList();

            return ret;

        }

        public List<Problem> GetForTourist(long touristId)
        {
            return _dbContext.Problems.Include(p => p.Tour).Where(p => p.UserId == touristId).ToList();
        }

        public List<Problem> GetRevisionForAdmin(long authorId)
        {
            return _dbContext.Problems.Include(p => p.Tour).Where(p => p.Status == ProblemStatus.ON_REVISION).ToList();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
