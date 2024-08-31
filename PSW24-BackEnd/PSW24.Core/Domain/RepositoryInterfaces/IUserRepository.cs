using Microsoft.EntityFrameworkCore;
using Quartz.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Core.Domain.RepositoryInterfaces
{
    public interface IUserRepository
    {
        User GetById(long id);
        User? GetActiveByName(string username);
        bool Exists(string username);
        User Create(User user);
        List<User> GetAllAuthor();
        void Save();
        List<User> GetAllTouristByInterest(long interestId);
    }
}
