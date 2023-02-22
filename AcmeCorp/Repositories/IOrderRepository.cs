using AcmeCorps.Data.Models;

namespace AcmeCorp.Repositories
{
    public interface IOrderRepository
    {
        Task DeleteOrder(int id);
        Task<Order> GetOrder(int id);
        Task<Order> PostOrder(Order order);
        Task PutOrder(int id, Order order);
        bool OrderExists(int id);
    }
}
