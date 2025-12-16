using Data.Models;

namespace Cmd._1_FilteringAndOrdering;

public class WhereConditions : QueryRunner
{
    public override void Run()
    {
        SingleCondition_Q();
        SingleCondition_F();

        SingleFunctionCondition_Q();
        SingleFunctionCondition_F();

        MultipleConditions_Q();
        MultiplesConditions_F();

        SingleOrderByDescending_Q();
        SingleOrderByDescending_F();

        MultipleOrderBy_Q();
        MultipleOrderBy_F();
        
        OrderByCustomComparer_F();
    }

    #region 1. Using A SIMPLE WHERE CLAUSE

    // Single condition, query syntax
    private void SingleCondition_Q()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result =
            from movie in sourceMovies
            where movie.Name.Contains("Spider-Man")
            select movie;

        PrintAll(result);
    }

    // Single condition, fluent syntax
    private void SingleCondition_F()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result = sourceMovies
            .Where(movie => movie.Name.Contains("Spider-Man"));

        PrintAll(result);
    }

    #endregion

    #region 2. REFACTORING STATEMENTS INTO FUNCTIONS

    // Single condition from a function, query syntax
    private void SingleFunctionCondition_Q()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result =
            from movie in sourceMovies
            where IsSpiderManMovie(movie)
            select movie;

        PrintAll(result);
    }

    // Single condition from a function, fluent syntax
    private void SingleFunctionCondition_F()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result = sourceMovies.Where(IsIronManMovie);

        PrintAll(result);
    }

    private static bool IsSpiderManMovie(Movie movie)
    {
        return movie.Name.Contains("Spider-Man");
    }

    private static bool IsIronManMovie(Movie movie)
    {
        return movie.Name.Contains("Iron");
    }

    #endregion

    #region 3. CHAINING MULTIPLE WHERE CLAUSES

    // Multiple chained conditions, query syntax
    private void MultipleConditions_Q()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result =
            from movie in sourceMovies
            where movie.Name.Contains("Spider-Man")
            where movie.ReleaseDate.Year < 2020
            select movie;

        PrintAll(result);
    }

    // Multiple chained conditions, fluent syntax
    private void MultiplesConditions_F()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result = sourceMovies
            .Where(movie => movie.Name.Contains("Iron"))
            .Where(movie => movie.ReleaseDate.Year < 2020);

        PrintAll(result);
    }

    #endregion

    #region 4. ORDERING DATA

    // Single order by, query syntax
    private void SingleOrderByDescending_Q()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result =
            from movie in sourceMovies
            orderby movie.Name descending
            select movie;

        PrintAll(result);
    }

    // Single order by, fluent syntax
    private void SingleOrderByDescending_F()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result = sourceMovies
            .OrderByDescending(movie => movie.Name);

        PrintAll(result);
    }

    #endregion

    #region 5. MULTI-LEVEL ORDERING

    // Multiple order by, query syntax
    private void MultipleOrderBy_Q()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result =
            from movie in sourceMovies
            orderby movie.ReleaseDate.Year descending, movie.Name
            select movie;

        PrintAll(result);
    }

    // Multiple order by, fluent syntax
    private void MultipleOrderBy_F()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result = sourceMovies
            .OrderByDescending(movie => movie.ReleaseDate.Year)
            .ThenBy(movie => movie.Name);

        PrintAll(result);
    }

    #endregion

    #region 6. USING A CUSTOM COMPARER TO ORDERING

    private void OrderByCustomComparer_F()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result = sourceMovies
            .OrderBy(movie => movie, new MovieComparer());

        PrintAll(result);
    }

    private class MovieComparer : IComparer<Movie>
    {
        public int Compare(Movie? first, Movie? second)
        {
            // Same instance
            if (ReferenceEquals(first, second)) return 0;

            // Null is smaller than everything
            if (first is null) return -1;

            if (second is null) return 1;

            // If the years are different, sort by year
            if (first.ReleaseDate.Year < second.ReleaseDate.Year)
                return -1;

            if (first.ReleaseDate.Year > second.ReleaseDate.Year)
                return 1;

            // If the years are equal, sort by name
            return string.Compare(first.Name, second.Name, StringComparison.Ordinal);
        }
    }

    #endregion
}