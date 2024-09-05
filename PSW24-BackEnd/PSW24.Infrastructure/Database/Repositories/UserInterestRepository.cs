using Microsoft.EntityFrameworkCore;
using PSW24.Core.Domain;
using PSW24.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Infrastructure.Database.Repositories
{
    public class UserInterestRepository : IUserInterestRepository
    {
        private readonly Context _dbContext;
        public UserInterestRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public UserInterest Create(UserInterest userInterest)
        {
            _dbContext.Add<UserInterest>(userInterest);
            _dbContext.SaveChanges();
            return userInterest;
        }

        public void Delete(long userId, long interestId)
        {
            UserInterest userInterest = _dbContext.UserInterests.FirstOrDefault(u => u.UserId == userId && u.InterestId == interestId);
            _dbContext.UserInterests.Remove(userInterest);
        }
    }
}
