using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieRental
{
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
            var rentsForCustomer = rents.GetRentsForCustomer(customer);
            var totalAmount = CalcAmountWithReport(rentsForCustomer, report);
            var frequentRenterPoints = CalcRenterPoints(rentsForCustomer);
            report.Append(
                $"Сумма задолженности составляет {totalAmount}\nВы заработали {frequentRenterPoints} очков за активность");
            return report.ToString();
        }

        private double CalcAmountWithReport(IEnumerable<Rental> rents, StringBuilder report)
        {
            double totalAmount = 0;

            foreach (var item in rents)
            {
                var thisAmount = _priceStrategies[item.Movie.PriceCode].GetPriceForDays(item.DaysRented);
                report.Append($"\t{item.Movie}\t{thisAmount}\n");
                totalAmount += thisAmount;
            }

            return totalAmount;
        }

        private double CalcRenterPoints(IEnumerable<Rental> rents)
        {
            var frequentRenterPoints = 0;

            foreach (var item in rents)
            foreach (var renterPointsStrategy in _renterPointsStrategies)
                frequentRenterPoints += renterPointsStrategy.GetRenterPoints(item.Movie.PriceCode, item.DaysRented);

            return frequentRenterPoints;
        }
    }
}