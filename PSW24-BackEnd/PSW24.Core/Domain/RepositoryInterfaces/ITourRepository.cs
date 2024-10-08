﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Core.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {
        Tour Create(Tour tour);
        List<Tour> GetAll();
        Tour Get(long id);
        void Save();
    }
}
