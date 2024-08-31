using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Core.Domain.RepositoryInterfaces
{
    public interface IKeyPointRepository
    {
        KeyPoint Create(KeyPoint keyPoint);
        List<KeyPoint> GetAllForTour(long tourId);
        List<KeyPoint> GetAll();
    }
}
