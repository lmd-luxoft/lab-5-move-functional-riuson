// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation

namespace MovieRental
{
    public class Rental
    {
        public Rental(Movie movie, int daysRental)
        {
            Movie = movie;
            DaysRented = daysRental;
        }

        public Movie Movie { get; }

        public int DaysRented { get; }
    }
}