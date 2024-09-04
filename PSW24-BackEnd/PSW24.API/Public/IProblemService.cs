using FluentResults;
using PSW24.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.API.Public
{
    public interface IProblemService
    {
        Result<ProblemDto> Create(ProblemDto dto);
        Result<List<ProblemDto>> GetForAuthor(long authorId);
        Result<List<ProblemDto>> GetNewForAuthor(long authorId);
        Result<ProblemDto> Solve(long problemId);
        Result<ProblemDto> Revision(long problemId);
        Result<List<ProblemDto>> GetRevisionForAdmin(long authorId);
        Result<ProblemDto> OnHold(long problemId);
        Result<ProblemDto> Reject(long problemId);
        Result<List<ProblemDto>> GetForTourist(long touristId);

    }
}
