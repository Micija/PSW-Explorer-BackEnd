using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Core.Domain.RepositoryInterfaces
{
    public interface IProblemLoggerRepository
    {
        ProblemLogger Create(ProblemLogger problemLogger);
    }
}
