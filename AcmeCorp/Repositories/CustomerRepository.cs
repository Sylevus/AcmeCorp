using AcmeCorps.Data;
using AcmeCorps.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AcmeCorp.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private AcmeCorpContext _context;

        public CustomerRepository(AcmeCorpContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _context.Customer.ToListAsync();
        }

        public async Task<Customer> GetCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);

            return customer;
        }

        public async Task PutCustomer(int id, Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();          
        }

        public async Task<Customer> PostCustomer(Customer customer)
        {
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task DeleteCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return;
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustomerId == id);
        }
    }
}
