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

        public Cart(long tourId, long buyerId)
        {
            BuyerId = buyerId;
            TourId = tourId;
        }
    }
}
