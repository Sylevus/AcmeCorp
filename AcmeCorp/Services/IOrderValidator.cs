using AcmeCorps.Data.Models;

namespace AcmeCorp.Services
{
    public interface IOrderValidator
    {
        public IEnumerable<string> ValidateOrder(Order order);
    }
}
