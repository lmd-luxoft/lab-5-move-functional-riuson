// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System;

namespace MovieRental
{
    internal class Rental
    {
        private Movie movie;
        private int daysRental;

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