using PSW24.Core.Domain;
using PSW24.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Infrastructure.Database.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly Context _dbContext;
        public CartRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public Cart GetById(long id)
        {
            Cart cart = _dbContext.Carts.FirstOrDefault(cart => cart.Id == id);
            if (cart == null) throw new KeyNotFoundException("Not found.");
            return cart;
        }

        public Cart Create(Cart cart)
        {
            _dbContext.Carts.Add(cart);
            _dbContext.SaveChanges();
            return cart;
        }
    }
}
