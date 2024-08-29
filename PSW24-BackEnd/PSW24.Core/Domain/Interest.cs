using PSW24.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Core.Domain
{
    public class Interest : Entity
    {
        public Interest Type { get; private set; }    
        public List<UserInterest> Users { get; private set; }
        public Interest(Interest type) { 
            Type = type;
        }
    }
}
