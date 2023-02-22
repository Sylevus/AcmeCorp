namespace AcmeCorps.Data.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        //public string PhoneNumber { get; set; }
        //public string Address { get; set; }
        //public string City { get; set; }
        //public string State { get; set; }   
        //public string Zipcode { get; set; }

        public ICollection<Order> Orders { get; set; }
        //In a real projec Address would break out into a join table with address type and customer id in a bridge table
        //In a real project I would do the same for phones and email
    }
}
