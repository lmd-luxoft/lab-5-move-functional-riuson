// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation

namespace MovieRental
{
    public class Customer
    {
        public Customer(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other is null)
                return false;

            if (!(other is Customer c))
                return false;

            return Name == c.Name;
        }

        public override int GetHashCode()
        {
            return Name?.GetHashCode() ?? 0;
        }

        public static bool operator !=(Customer obj1, Customer obj2)
        {
            return !(obj1 == obj2);
        }

        public static bool operator ==(Customer obj1, Customer obj2)
        {
            return obj1?.Equals(obj2) ?? false;
        }
    }
}