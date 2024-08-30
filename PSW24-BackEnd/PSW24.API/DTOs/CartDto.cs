using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.API.DTOs
{
    public  class CartDto
    {
        public long TourId { get;  set; }
        public long BuyerId { get;  set; }
        public bool Bought { get; set; }    
    }
}
