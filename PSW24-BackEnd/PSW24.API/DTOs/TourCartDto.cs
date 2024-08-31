using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.API.DTOs
{
    public class TourCartDto
    {
        public TourDto Tour { get; set; }
        public long CartId { get; set; }    
    }
}
