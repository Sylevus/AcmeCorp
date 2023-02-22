using AcmeCorp.Services;
using AcmeCorps.Data;
using AcmeCorps.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AcmeCorp.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AcmeCorpContext _context;
        private readonly IOrderValidator _orderValidator;

        public OrderRepository(
            AcmeCorpContext context,
            IOrderValidator orderValidator)
        {
            _context = context;
            _orderValidator = orderValidator;
        }

        public async Task DeleteOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return;
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);

            return order;
        }

        public bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.CustomerId == id);
        }

        public async Task<Order> PostOrder(Order order)
        {
            ValidateOrder(order);

            _context.Order.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task PutOrder(int id, Order order)
        {
            ValidateOrder(order);
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        private void ValidateOrder(Order order)
        {
            var errors = _orderValidator.ValidateOrder(order);
            if (errors.Any())
            {
                throw new Exception("Errors found in order " + String.Join(",", errors.ToArray()));
            }
        }
    }
}
