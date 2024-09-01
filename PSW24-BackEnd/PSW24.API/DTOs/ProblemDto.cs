using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.API.DTOs
{
    public class ProblemDto
    {
        public long Id { get; set; }
        public long TourId { get;  set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public string Status { get;  set; }
        public long UserId { get;  set; }
    }
}
