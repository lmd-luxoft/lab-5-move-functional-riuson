namespace MovieRental
{
    public abstract class PriceStrategy
    {
        public PriceStrategy(Movie.Type movieType)
        {
            MovieType = movieType;
        }

        public Movie.Type MovieType { get; }

        public abstract double GetPriceForDays(int daysRented);
    }
}