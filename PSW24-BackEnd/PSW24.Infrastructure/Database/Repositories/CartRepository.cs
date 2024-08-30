using Microsoft.EntityFrameworkCore;
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

        public Cart Delete(Cart cart)
        {
            _dbContext.Carts.Remove(cart);
            _dbContext.SaveChanges();
            return cart;
        }

        public Cart Get(long cartId)
        {
            return _dbContext.Carts.FirstOrDefault(c => c.Id == cartId);
        }

        public List<long> GetCartCustomer(User customer)
        {
            return _dbContext.Carts.Where(c => c.BuyerId == customer.Id && !c.Bought).Select(c => c.TourId).ToList();
        }

        public List<Cart> GetCustomer(User customer)
        {
            return _dbContext.Carts.Include(c => c.Tour).Where(c => c.BuyerId == customer.Id).ToList();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
