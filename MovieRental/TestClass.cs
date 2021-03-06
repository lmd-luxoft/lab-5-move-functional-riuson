﻿// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation

using System.Linq;
using NUnit.Framework;

namespace MovieRental
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void NameFilmShouldBeCorrect()
        {
            var movie = new Movie("Rio2", Movie.Type.NEW_RELEASE);
            Assert.AreEqual("Rio2", movie.Title);
        }

        [Test]
        public void TypeFilmShouldBeCorrect()
        {
            var movie = new Movie("Rio2", Movie.Type.NEW_RELEASE);
            Assert.AreEqual(Movie.Type.NEW_RELEASE, movie.PriceCode);
        }

        [Test]
        public void RentalShouldBeCorrectMovie()
        {
            var movie = new Movie("Angry Birds", Movie.Type.REGULAR);
            var rental = new Rental(movie, 20);
            Assert.AreEqual(movie, rental.Movie);
        }

        [Test]
        public void RentalShouldBeCorrectDayRented()
        {
            var movie = new Movie("Angry Birds", Movie.Type.REGULAR);
            var rental = new Rental(movie, 20);
            Assert.AreEqual(20, rental.DaysRented);
        }

        [Test]
        public void CustomerShouldBeCorrectName()
        {
            var customer = new Customer("Bug");
            Assert.AreEqual("Bug", customer.Name);
        }

        [TestCase("name", "name", ExpectedResult = true)]
        [TestCase("name1", "name2", ExpectedResult = false)]
        public bool CustomersShoudSuuportEquality(string name1, string name2)
        {
            // Arrange.
            var customer1 = new Customer(name1);
            var customer2 = new Customer(name2);

            // Act & Assert.
            return customer1 == customer2;
        }

        [Test]
        public void RentsShouldDistinctPerCustomers()
        {
            // Arrange.
            var customer1 = new Customer("Bug");
            var customer2 = new Customer("Cat");

            var rents = new Rents();

            var movie1 = new Movie("Angry Birds", Movie.Type.CHILDREN);
            rents.Add(customer1, movie1, 2);

            var movie2 = new Movie("StarWar", Movie.Type.NEW_RELEASE);
            rents.Add(customer1, movie2, 3);

            var movie3 = new Movie("Hatico", Movie.Type.REGULAR);
            rents.Add(customer1, movie3, 4);

            var movie4 = new Movie("Cauldron", Movie.Type.CHILDREN);
            rents.Add(customer2, movie4, 5);

            var movie5 = new Movie("Accelerando", Movie.Type.NEW_RELEASE);
            rents.Add(customer2, movie5, 6);

            var movie6 = new Movie("The Three-Body Problem", Movie.Type.REGULAR);
            rents.Add(customer2, movie6, 7);

            // Act.
            var rents1 = rents.GetRentsForCustomer(customer1).ToArray();
            var rents2 = rents.GetRentsForCustomer(customer2).ToArray();

            // Assert.
            Assert.That(rents1.Length, Is.EqualTo(3));
            Assert.That(rents1[0].Movie, Is.EqualTo(movie1));
            Assert.That(rents1[1].Movie, Is.EqualTo(movie2));
            Assert.That(rents1[2].Movie, Is.EqualTo(movie3));
            Assert.That(rents1[0].DaysRented, Is.EqualTo(2));
            Assert.That(rents1[1].DaysRented, Is.EqualTo(3));
            Assert.That(rents1[2].DaysRented, Is.EqualTo(4));

            Assert.That(rents2.Length, Is.EqualTo(3));
            Assert.That(rents2[0].Movie, Is.EqualTo(movie4));
            Assert.That(rents2[1].Movie, Is.EqualTo(movie5));
            Assert.That(rents2[2].Movie, Is.EqualTo(movie6));
            Assert.That(rents2[0].DaysRented, Is.EqualTo(5));
            Assert.That(rents2[1].DaysRented, Is.EqualTo(6));
            Assert.That(rents2[2].DaysRented, Is.EqualTo(7));
        }

        [Test]
        public void CustomerCreateCorrectStatement()
        {
            var customer = new Customer("Bug");
            var rents = new Rents();

            var movie1 = new Movie("Angry Birds", Movie.Type.CHILDREN);
            rents.Add(customer, movie1, 2);

            var movie2 = new Movie("StarWar", Movie.Type.NEW_RELEASE);
            rents.Add(customer, movie2, 10);

            var movie3 = new Movie("Hatico", Movie.Type.REGULAR);
            rents.Add(customer, movie3, 4);

            var statements = StatementsFactory.Create();

            var actual = statements.GetStatement(rents, customer);
            Assert.AreEqual(
                "учет аренды для Bug\n\tAngry Birds\t15\n\tStarWar\t30\n\tHatico\t32\nСумма задолженности составляет 77\nВы заработали 4 очков за активность",
                actual);
        }
    }
}