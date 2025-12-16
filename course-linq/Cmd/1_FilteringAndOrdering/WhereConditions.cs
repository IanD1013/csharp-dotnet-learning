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
    }

    /// USING A SIMPLE WHERE CLAUSE:
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

    /// REFACTORING STATEMENTS INTO FUNCTIONS
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

    /// <summary>
    /// Multiple chained conditions, query syntax
    /// </summary>
    private void MultipleConditions_Q()
    {
        var sourceMovies = Repository.GetAllMovies();
    }

    /// <summary>
    /// Multiple chained conditions, fluent syntax
    /// </summary>
    private void MultiplesConditions_F()
    {
        var sourceMovies = Repository.GetAllMovies();
    }
}