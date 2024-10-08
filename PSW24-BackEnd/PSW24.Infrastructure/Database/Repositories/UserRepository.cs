﻿using PSW24.Core.Domain;
using PSW24.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace PSW24.Infrastructure.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _dbContext;
         public UserRepository(Context dbContext) {
            _dbContext = dbContext;
        }

        public User GetById(long id)
        {
            User user = _dbContext.Users.Include(u => u.Interests).FirstOrDefault(user => user.Id == id);
            if (user == null) throw new KeyNotFoundException("Not found.");
            return user;
        }

        public User? GetActiveByName(string username)
        {
            return _dbContext.Users.FirstOrDefault(user => user.Username == username && user.IsActive);
        }

        public bool Exists(string username)
        {
            return _dbContext.Users.Any(user => user.Username == username);
        }

        public User Create(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user;
        }

        public List<User> GetAllAuthor()
        {
            return _dbContext.Users.Include(u => u.Tours).Where(u => u.Role == UserRole.Author).ToList();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public List<User> GetAllTouristByInterest(long interestId)
        {
            return _dbContext.Users.Include(u => u.Interests).Where(u => u.Interests.FirstOrDefault(i => i.InterestId == interestId) == null ? false : true).ToList();
        }

        public List<User> GetSuspicious()
        {
            return _dbContext.Users.Where(u => u.Penalty >= 10 && u.IsBlocked == false).ToList();
        }

        public List<User> GetBlocked()
        {
            return _dbContext.Users.Where(u => u.IsBlocked).ToList();
        }
    }
}
