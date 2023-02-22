using AcmeCorps.Data.Models;

namespace AcmeCorp.Services
{
    public class OrderValidator : IOrderValidator
    {
        public IEnumerable<string> ValidateOrder(Order order)
        {
            List<string> errors = new List<string>();

            if (order.Tax > order.Price)
            {
                errors.Add("Tax should not be more than order price");
            }

            if(order.Total != order.Tax + order.Price)
            {
                errors.Add("Total amount is incorrect");
            }

            return errors;
        }
    }
}
