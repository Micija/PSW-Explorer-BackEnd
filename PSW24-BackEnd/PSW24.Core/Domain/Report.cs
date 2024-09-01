using PSW24.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Core.Domain
{
    public class Report : Entity
    {
        public long UserId { get; private set; }
        public User User { get; private set; }
        public string Path { get; private set; }

        public Report(long userId, User user, string path)
        {
            UserId = userId;
            User = user;
            Path = path;
        }
    }
}
