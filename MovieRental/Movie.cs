// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System;

namespace MovieRental
{
    public class Movie
    {
        private string title;
        private Type type;

        public Movie(string title, Type type)
        {
            this.title = title;
            this.type = type;
        }

        public enum Type
        {
            NEW_RELEASE,
            REGULAR,
            CHILDREN
        }

        public string getTitle()
        {
            return title;
        }
        public Type getPriceCode()
        {
            return type;
        }
        public override string ToString()
        {
            return title;
        }
    }
}