using PSW24.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Core.Domain
{
    public class Cart : Entity
    {
        public long TourId { get; private set; }
        public Tour Tour { get; private set; }
        public long BuyerId { get; private set; }
        public User Buyer { get; private set; }
        public bool Bought { get; private set; }
        public DateTime? Date { get; private set; }
        public Cart(long tourId, long buyerId, bool bought)
        {
            BuyerId = buyerId;
            TourId = tourId;
            Bought = bought;
        }
        public void Buy()
        {
            Bought = true;
        }
        public void Now()
        {
            Date = DateTime.Now;
        }
    }
}
