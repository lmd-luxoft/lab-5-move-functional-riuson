namespace MovieRental
{
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