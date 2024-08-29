﻿using PSW24.Core.Domain.RepositoryInterfaces;
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
    }
}
