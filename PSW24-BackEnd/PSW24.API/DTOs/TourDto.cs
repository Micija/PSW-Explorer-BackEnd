using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.API.DTOs
{
    public class TourDto
    {
        public long Id { get; set; }    
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Difficulty { get; private set; }
        public long InterestId { get; private set; }
        public double Price { get; private set; }
        public string Status { get; private set; }
        public long AuthorId { get; private set; }
    }
}
