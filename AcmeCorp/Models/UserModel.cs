﻿namespace AcmeCorp.Models
{
    public class UserModel
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> UserRoles { get; set; }
    }
}
