// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation

using System.Collections.Generic;
using System.Text;

namespace MovieRental
{
    public class Customer
    {
        private readonly List<Rental> rentals = new List<Rental>();

        public Customer(string name)
        {
            Name = name;
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
                switch (item.Movie.PriceCode)
                {
                    case Movie.Type.REGULAR:
                        thisAmount += 2;
                        if (item.DaysRented > 2)
                            thisAmount += (item.DaysRented - 2) * 15;
                        break;
                    case Movie.Type.NEW_RELEASE:
                        thisAmount += item.DaysRented * 3;
                        break;
                    case Movie.Type.CHILDREN:
                        thisAmount += 15;
                        if (item.DaysRented > 3)
                            thisAmount += (item.DaysRented - 3) * 15;
                        break;
                }

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
}