// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRental
{
    public class Customer
    {
        List<Rental> rentals = new List<Rental>();
        private string name;

        public Customer(string name)
        {
            this.name = name;
        }
        public string getName()
        {
            return name;
        }

        internal void addRental(Rental rental)
        {
           rentals.Add(rental);
        }

        internal string statement()
        {
            StringBuilder report = new StringBuilder();
            report.Append($"учет аренды для {getName()}\n");
            double totalAmount = 0;
            
            int frequentRenterPoints = 0;
            foreach (var item in rentals)
            {
                double thisAmount = 0;
                switch (item.getMovie().getPriceCode())
                {
                    case Movie.Type.REGULAR:
                        thisAmount += 2;
                        if(item.getDaysRented() > 2)
                            thisAmount += (item.getDaysRented() - 2) * 15;
                        break;
                    case Movie.Type.NEW_RELEASE:
                        thisAmount += item.getDaysRented() * 3;
                        break;
                    case Movie.Type.CHILDREN:
                        thisAmount += 15;
                        if(item.getDaysRented() > 3)
                            thisAmount += (item.getDaysRented() - 3) * 15;
                        break;
                }
                
                //добавить очки для активного арендатора
                frequentRenterPoints++;
                //бонус за аренду новинки на два дня
                if (item.getMovie().getPriceCode() == Movie.Type.NEW_RELEASE && item.getDaysRented() > 1)
                    frequentRenterPoints++;
                report.Append($"\t{item.getMovie()}\t{thisAmount}\n");
               
                totalAmount += thisAmount;
            }
            report.Append($"Сумма задолженности составляет {totalAmount}\nВы заработали {frequentRenterPoints} очков за активность");
            return report.ToString();
        }
    }
}