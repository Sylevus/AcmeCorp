using AcmeCorp.Repositories;
using AcmeCorp.Services;
using AcmeCorps.Data;
using AcmeCorps.Data.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorp.Tests
{
    [TestClass]
    public class OrderTests
    {

        //Example of Unit Tests
        [TestMethod]
        public void TestOrder_ValidatesCorrectly()
        {
            var order = new Order() { Price = 8, Tax = 2, Total = 10 };
            var validator = new OrderValidator();
            var errors = validator.ValidateOrder(order);
            Assert.IsTrue(errors.Count() == 0);
        }

        [TestMethod]
        public void TestOrderTotalAddedIncorrectly()
        {
            var order = new Order() { Price = 8, Tax = 2, Total = 11 };
            var validator = new OrderValidator();
            var errors = validator.ValidateOrder(order);
            Assert.IsTrue(errors.Any(x => x.Contains("Total amount is incorrect")));
            Assert.IsTrue(errors.Count() >0);
        }

        [TestMethod]
        public void TestOrderTaxSetIncorrectly()
        {
            var order = new Order() { Price = 8, Tax = 12, Total = 20 };
            var validator = new OrderValidator();
            var errors = validator.ValidateOrder(order);
            Assert.IsTrue(errors.Any(x => x.Contains("Tax should not be more than order price")));
            Assert.IsTrue(errors.Count() > 0);
        }
    }
}
