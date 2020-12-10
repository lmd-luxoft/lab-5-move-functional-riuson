// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace MovieRental
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void NameFilmShouldBeCorrect()
        {
	        Movie movie = new Movie("Rio2", Movie.Type.NEW_RELEASE);
            Assert.AreEqual("Rio2", movie.getTitle());
        }
        [Test]
        public void TypeFilmShouldBeCorrect()
        {

            Movie movie = new Movie("Rio2", Movie.Type.NEW_RELEASE);
            Assert.AreEqual(Movie.Type.NEW_RELEASE, movie.getPriceCode());
        }
        [Test]
        public void RentalShouldBeCorrectMovie()
        {
            Movie movie = new Movie("Angry Birds", Movie.Type.REGULAR);
            Rental rental = new Rental(movie, 20);
            Assert.AreEqual(movie, rental.getMovie());
        }
        [Test]
        public void RentalShouldBeCorrectDayRented()
        {

            Movie movie = new Movie("Angry Birds", Movie.Type.REGULAR);
            Rental rental = new Rental(movie, 20);
            Assert.AreEqual(20, rental.getDaysRented());
        }
        [Test]
        public void CustomerShouldBeCorrectName()
        {
            Customer customer = new Customer("Bug");
            Assert.AreEqual("Bug", customer.getName());
        }
        [Test]
        public void CustomerCreateCorrectStatement()
{

    Customer customer = new Customer("Bug");

        Movie movie1 = new Movie("Angry Birds", Movie.Type.CHILDREN);
        Rental rental1 = new Rental(movie1, 2);
        customer.addRental(rental1);

	Movie movie2 = new Movie("StarWar", Movie.Type.NEW_RELEASE);
        Rental rental2 = new Rental(movie2, 10);
        customer.addRental(rental2);

	Movie movie3 = new Movie("Hatico", Movie.Type.REGULAR);
        Rental rental3 = new Rental(movie3, 4);
        customer.addRental(rental3);

        string actual = customer.statement();
        Assert.AreEqual("учет аренды для Bug\n\tAngry Birds\t15\n\tStarWar\t30\n\tHatico\t32\nСумма задолженности составляет 77\nВы заработали 4 очков за активность", actual);
    }
}
}
