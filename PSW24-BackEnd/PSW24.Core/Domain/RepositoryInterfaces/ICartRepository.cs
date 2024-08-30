using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Core.Domain.RepositoryInterfaces
{
    public interface ICartRepository
    {
        Cart Create(Cart cart);
        Cart Delete(Cart cart);
        Cart Get(long cartId);
        List<long> GetCartCustomer(User customer);
        List<Cart> GetCustomer(User customer);
        void Save();
    
    }
}
