using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.Teirlinck.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UtilsTests
{
    [TestClass]
    public class LambdaEqualityComparerTests
    {
        [TestMethod]
        public void TestComparer()
        {
            Customer customer1 = new Customer()
            {
                Id = new Guid("12345678901234567890123456789012"),
                Forename = "Bob"
            };

            Customer customer2 = new Customer()
            {
                Id = new Guid("12345678901234567890123456789012"),
                Surname = "Smith"
            };

            Customer customer3 = new Customer()
            {
                Id = new Guid("11111111111111111111111111111111"),
                Forename = "Bob"
            };

            var comparer = new LambdaEqualityComparer<Customer>((x, y) => x.Id == y.Id, x => x.Id.GetHashCode());

            Assert.IsTrue(comparer.Equals(customer1, customer2));
            Assert.IsFalse(comparer.Equals(customer1, customer3));

            Assert.AreEqual(comparer.GetHashCode(customer1), -1876532676);
            Assert.AreEqual(comparer.GetHashCode(customer2), -1876532676);
            Assert.AreEqual(comparer.GetHashCode(customer3), 285212689);

            var customers = new List<Customer> { customer1, customer2, customer3 };
            var uniqueCustomers = customers.Distinct(comparer);      // Returns customer1 and customer3

            Assert.IsTrue(uniqueCustomers.Count() == 2);
        }

        private class Customer
        {
            public Guid Id { get; set; }

            public string Forename { get; set; }

            public string Surname { get; set; }
        }
    }
}
