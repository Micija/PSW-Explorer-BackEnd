using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Core.Domain.RepositoryInterfaces
{
    public interface IProblemRepository
    {
        Problem Create(Problem problem);
        List<Problem> GetForAuthor(long authorId);
        List<Problem> GetNewForAuthor(long authorId);
        Problem GetById(long problemId);
        void Save();
        List<Problem> GetRevisionForAdmin(long authorId);
        List<Problem> GetForTourist(long touristId);
    }
}
