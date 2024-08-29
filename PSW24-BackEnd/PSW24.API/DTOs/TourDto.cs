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
        public string Name { get; set; }
        public string Description { get; set; }
        public string Difficulty { get; set; }
        public long InterestId { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public long AuthorId { get; set; }
    }
}
