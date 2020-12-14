// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation

namespace MovieRental
{
    internal class Rental
    {
        private readonly int daysRental;
        private readonly Movie movie;

        public Rental(Movie movie, int daysRental)
        {
            this.movie = movie;
            this.daysRental = daysRental;
        }

        internal Movie getMovie()
        {
            return movie;
        }

        internal int getDaysRented()
        {
            return daysRental;
        }
    }
}