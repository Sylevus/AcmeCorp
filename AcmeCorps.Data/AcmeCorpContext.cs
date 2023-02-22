using AcmeCorps.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AcmeCorps.Data
{
    public class AcmeCorpContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Order { get; set; }
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }
        public virtual DbSet<RolesMaster> RolesMaster { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<UsersMaster> UsersMaster { get; set; }

        public string DbPath { get; }

        string _database = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "AcmeCorp.db");

        public AcmeCorpContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={_database}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
              new Customer { CustomerId = 1, FirstName = "Darth", LastName = "Vader", EmailAddress = "Customer1@dev.com" },
              new Customer { CustomerId = 2, FirstName = "ObiWan", LastName = "Kenobi", EmailAddress = "Customer2@dev.com" },
              new Customer { CustomerId = 3, FirstName = "Luke", LastName = "Skywalker", EmailAddress = "Customer3@dev.com" }
            );

            modelBuilder.Entity<RolesMaster>().HasData(
                new RolesMaster() { RoleId = 1, RoleName = "Admin", CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now }
                );

            modelBuilder.Entity<UsersMaster>().HasData(
              new UsersMaster { UserId = 1, Email = "taylor.walston@comcast.net", FirstName = "Taylor", LastName = "Walston", Password = "Password", PhoneNumber = "5555555555", UserName = "twalston", CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now }
            );

            modelBuilder.Entity<UserRoles>().HasData(
                new UserRoles() { UserRolesId = 1, RoleId = 1, UserId = 1 }
            );
        }
    }
}
