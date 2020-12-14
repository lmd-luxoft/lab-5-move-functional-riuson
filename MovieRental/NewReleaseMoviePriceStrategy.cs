namespace MovieRental
{
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
}