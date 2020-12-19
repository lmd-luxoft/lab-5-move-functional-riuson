namespace MovieRental
{
    public interface IRenterPointsStrategy
    {
        int GetRenterPoints(Movie.Type movieType, int daysRented);
    }
}