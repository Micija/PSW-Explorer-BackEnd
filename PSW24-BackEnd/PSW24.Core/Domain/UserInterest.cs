using PSW24.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Core.Domain
{
    public class UserInterest : Entity
    {
        public long UserId { get; private set; }
        public long InterestId { get; private set; }

        public UserInterest(long userId, long interestId) 
        {
            UserId = userId;    
            InterestId = interestId;
        }
    }
}
