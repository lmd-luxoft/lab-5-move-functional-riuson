namespace MovieRental
{
    public class NewReleasesPer2DaysPointsStrategy : IRenterPointsStrategy
    {
        public int GetRenterPoints(Movie.Type movieType, int daysRented)
        {
            if (movieType == Movie.Type.NEW_RELEASE && daysRented > 1)
                return 1;

            return 0;
        }
    }
}