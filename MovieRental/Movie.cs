// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation

namespace MovieRental
{
    public class Movie
    {
        public enum Type
        {
            NEW_RELEASE,
            REGULAR,
            CHILDREN
        }

        private readonly string title;
        private readonly Type type;

        public Movie(string title, Type type)
        {
            this.title = title;
            this.type = type;
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