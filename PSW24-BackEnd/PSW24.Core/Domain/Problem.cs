using PSW24.BuildingBlocks.Core.Domain;
using PSW24.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Core.Domain
{
    public class Problem : Entity
    {
        public long TourId {  get; private set; }
        public Tour Tour { get; private set; }
        public string Name {  get; private set; }
        public string Description { get; private set; }
        public ProblemStatus Status { get; private set; }
        public long UserId {  get; private set; }
        public User User { get; private set; }
        public ICollection<ProblemLogger> Loggers { get; } = [];

        public Problem(long tourId, string name, string description, ProblemStatus status, long userId)
        {
            TourId = tourId;
            Name = name;
            Description = description;
            Status = status;
            UserId = userId;
        }

        public void Solve()
        {
            Status = ProblemStatus.SOLVED;
        }
    
        public void Revision()
        {
            Status = ProblemStatus.ON_REVISION;
        }

        public void OnHold()
        {
            Status = ProblemStatus.ON_HOLD;
        }

        public void Reject()
        {
            Status = ProblemStatus.REJECT;
        }
    }
}
