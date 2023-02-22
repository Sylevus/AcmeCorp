using AcmeCorps.Data.Models;

namespace AcmeCorp.Repositories
{
    public interface ICustomerRepository
    {
        Task DeleteCustomer(int id);
        Task<Customer> GetCustomer(int id);
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> PostCustomer(Customer customer);
        Task PutCustomer(int id, Customer customer);
        bool CustomerExists(int id);
    }
}