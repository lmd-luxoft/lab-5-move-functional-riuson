namespace MovieRental
{
    public class ActiveRenterPointsStrategy : IRenterPointsStrategy
    {
        public int GetRenterPoints(Movie.Type _, int __)
        {
            return 1;
        }
    }
}