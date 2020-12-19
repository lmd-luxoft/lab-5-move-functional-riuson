namespace MovieRental
{
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
}