// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieRental
{
    public class Customer
    {
        public Customer(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other is null)
                return false;

            if (!(other is Customer c))
                return false;

            return Name == c.Name;
        }

        public override int GetHashCode()
        {
            return Name?.GetHashCode() ?? 0;
        }

        public static bool operator !=(Customer obj1, Customer obj2)
        {
            return !(obj1 == obj2);
        }

        public static bool operator ==(Customer obj1, Customer obj2)
        {
            return obj1?.Equals(obj2) ?? false;
        }
    }

    public abstract class PriceStrategy
    {
        public PriceStrategy(Movie.Type movieType)
        {
            MovieType = movieType;
        }

        public Movie.Type MovieType { get; }

        public abstract double GetPriceForDays(int daysRented);
    }

    public class RegularMoviePriceStrategy : PriceStrategy
    {
        public RegularMoviePriceStrategy() : base(Movie.Type.REGULAR)
        {
        }

        public override double GetPriceForDays(int daysRented)
        {
            double result = 2;

            if (daysRented > 2)
                result += (daysRented - 2) * 15;

            return result;
        }
    }

    public class NewReleaseMoviePriceStrategy : PriceStrategy
    {
        public NewReleaseMoviePriceStrategy() : base(Movie.Type.NEW_RELEASE)
        {
        }

        public override double GetPriceForDays(int daysRented)
        {
            return daysRented * 3;
        }
    }

    public class ChildrenMoviePriceStrategy : PriceStrategy
    {
        public ChildrenMoviePriceStrategy() : base(Movie.Type.CHILDREN)
        {
        }

        public override double GetPriceForDays(int daysRented)
        {
            double result = 15;

            if (daysRented > 3)
                result += (daysRented - 3) * 15;

            return result;
        }
    }

    public interface IRenterPointsStrategy
    {
        int GetRenterPoints(Movie.Type movieType, int daysRented);
    }

    public class ActiveRenterPointsStrategy : IRenterPointsStrategy
    {
        public int GetRenterPoints(Movie.Type _, int __)
        {
            return 1;
        }
    }

    public class NewReleasesPer2DaysPointsStrategy : IRenterPointsStrategy
    {
        public int GetRenterPoints(Movie.Type movieType, int daysRented)
        {
            if (movieType == Movie.Type.NEW_RELEASE && daysRented > 1)
                return 1;

            return 0;
        }
    }

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

    public class Statements
    {
        private readonly IDictionary<Movie.Type, PriceStrategy> _priceStrategies;
        private readonly IEnumerable<IRenterPointsStrategy> _renterPointsStrategies;

        public Statements(
            IEnumerable<PriceStrategy> priceStrategies,
            IEnumerable<IRenterPointsStrategy> renterPointsStrategies)
        {
            _priceStrategies = priceStrategies.ToDictionary(x => x.MovieType);
            _renterPointsStrategies = renterPointsStrategies;
        }

        public string GetStatement(Rents rents, Customer customer)
        {
            var report = new StringBuilder();
            report.Append($"учет аренды для {customer.Name}\n");
            double totalAmount = 0;

            var frequentRenterPoints = 0;
            foreach (var item in rents.GetRentsForCustomer(customer))
            {
                double thisAmount = 0;

                thisAmount += _priceStrategies[item.Movie.PriceCode].GetPriceForDays(item.DaysRented);

                foreach (var renterPointsStrategy in _renterPointsStrategies)
                    frequentRenterPoints += renterPointsStrategy.GetRenterPoints(item.Movie.PriceCode, item.DaysRented);

                report.Append($"\t{item.Movie}\t{thisAmount}\n");

                totalAmount += thisAmount;
            }

            report.Append(
                $"Сумма задолженности составляет {totalAmount}\nВы заработали {frequentRenterPoints} очков за активность");
            return report.ToString();
        }
    }

    public static class StatementsFactory
    {
        public static Statements Create()
        {
            return new Statements(
                new PriceStrategy[]
                {
                    new ChildrenMoviePriceStrategy(),
                    new NewReleaseMoviePriceStrategy(),
                    new RegularMoviePriceStrategy()
                },
                new IRenterPointsStrategy[]
                {
                    new ActiveRenterPointsStrategy(),
                    new NewReleasesPer2DaysPointsStrategy()
                }
            );
        }
    }
}