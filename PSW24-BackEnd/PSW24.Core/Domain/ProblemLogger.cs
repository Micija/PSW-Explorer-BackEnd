using PSW24.BuildingBlocks.Core.Domain;
using PSW24.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Core.Domain
{
    public class ProblemLogger : Entity
    {
        public long ProblemId { get; set; }
        public Problem Problem { get; set; }    
        public ProblemStatus New {  get; set; } 
        public ProblemStatus Old { get; set; }

        public ProblemLogger(long problemId, ProblemStatus @new, ProblemStatus old)
        {
            ProblemId = problemId;
            New = @new;
            Old = old;
        }
    }
}
