using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.BuildingBlocks.Core.Domain
{
    public class DomainEvent : ValueObject
    {
        public DomainEvent() { }

        [JsonConstructor]
        public DomainEvent(long id,DateTime eventTime,string context)
        {
            Id = id;
            EventTime= eventTime;
            Context = context; 
        }

        public long Id { get; }
        public DateTime EventTime { get; }
        public string Context { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return EventTime;
            yield return Context;


        }
    }
}
