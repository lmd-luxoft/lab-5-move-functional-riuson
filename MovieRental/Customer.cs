﻿// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation

using System.Collections.Generic;
using System.Text;

namespace MovieRental
{
    public class Customer
    {
        private readonly List<Rental> rentals = new List<Rental>();

        private readonly Dictionary<Movie.Type, PriceStrategy> _priceStrategies =
            new Dictionary<Movie.Type, PriceStrategy>();

        public Customer(string name)
        {
            Name = name;
            _priceStrategies.Add(Movie.Type.CHILDREN, new ChildrenMoviePriceStrategy());
            _priceStrategies.Add(Movie.Type.NEW_RELEASE, new NewReleaseMoviePriceStrategy());
            _priceStrategies.Add(Movie.Type.REGULAR, new RegularMoviePriceStrategy());
        }

        public string Name { get; }

        internal void addRental(Rental rental)
        {
            rentals.Add(rental);
        }

        internal string statement()
        {
            var report = new StringBuilder();
            report.Append($"учет аренды для {Name}\n");
            double totalAmount = 0;

            var frequentRenterPoints = 0;
            foreach (var item in rentals)
            {
                double thisAmount = 0;

                thisAmount += _priceStrategies[item.Movie.PriceCode].GetPriceForDays(item.DaysRented);

                //добавить очки для активного арендатора
                frequentRenterPoints++;
                //бонус за аренду новинки на два дня
                if (item.Movie.PriceCode == Movie.Type.NEW_RELEASE && item.DaysRented > 1)
                    frequentRenterPoints++;
                report.Append($"\t{item.Movie}\t{thisAmount}\n");

                totalAmount += thisAmount;
            }

            report.Append(
                $"Сумма задолженности составляет {totalAmount}\nВы заработали {frequentRenterPoints} очков за активность");
            return report.ToString();
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
}