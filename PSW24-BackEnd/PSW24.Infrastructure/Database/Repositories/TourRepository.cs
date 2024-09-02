using Microsoft.EntityFrameworkCore;
using PSW24.BuildingBlocks.Infrastructure.Database;
using PSW24.Core.Domain;
using PSW24.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Infrastructure.Database.Repositories
{
    public class TourRepository : ITourRepository
    {
        private readonly Context _dbContext;
        public TourRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public Tour Create(Tour tour)
        {
            _dbContext.Tours.Add(tour);
            _dbContext.SaveChanges();
            return tour;
        }

        public List<Tour> GetAll()
        {
            return _dbContext.Tours.Include(t => t.KeyPoints).Include(t => t.Author).ToList();
        }

        
        public Tour Get(long id)
        {
            return _dbContext.Tours.Include(t => t.KeyPoints).FirstOrDefault(t => t.Id == id);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

    }
}
