namespace AcmeCorps.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Price { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public int PaymentTypeId { get; set; }
        public int PaymentAccountNumber { get; set; }

        //Real Proiject would have OrderItems broken out, Pricing tables, Item type tables, etc
    }
}
