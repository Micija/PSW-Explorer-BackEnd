﻿using PSW24.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Infrastructure.Database.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly Context _dbContext;
        public ReportRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
