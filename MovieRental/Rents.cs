using System.Collections.Generic;
using System.Linq;

namespace MovieRental
{
    public class Rents
    {
        private readonly List<CustomerRent> _rents = new List<CustomerRent>();

        public void Add(Customer customer, Rental rental)
        {
            _rents.Add(new CustomerRent(customer, rental));
        }

        public void Add(Customer customer, Movie movie, int days)
        {
            Add(customer, new Rental(movie, days));
        }

        public IEnumerable<Rental> GetRentsForCustomer(Customer customer)
        {
            return _rents
                .Where(x => x.Customer == customer)
                .Select(x => x.Rental)
                .ToArray();
        }

        private class CustomerRent
        {
            public CustomerRent(Customer customer, Rental rental)
            {
                Rental = rental;
                Customer = customer;
            }

            public Rental Rental { get; }
            public Customer Customer { get; }
        }
    }
}