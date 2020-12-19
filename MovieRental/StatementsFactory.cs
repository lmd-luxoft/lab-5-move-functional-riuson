namespace MovieRental
{
    public static class StatementsFactory
    {
        public static Statements Create()
        {
            return new Statements(
                new PriceStrategy[]
                {
                    new ChildrenMoviePriceStrategy(),
                    new NewReleaseMoviePriceStrategy(),
                    new RegularMoviePriceStrategy()
                },
                new IRenterPointsStrategy[]
                {
                    new ActiveRenterPointsStrategy(),
                    new NewReleasesPer2DaysPointsStrategy()
                }
            );
        }
    }
}