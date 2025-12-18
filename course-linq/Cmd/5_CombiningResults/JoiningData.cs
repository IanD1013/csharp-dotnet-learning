// ReSharper disable UnusedMember.Local

namespace Cmd._5_CombiningResults;

public class JoiningData : QueryRunner
{
    public override void Run()
    {
        // InnerJoin_Q();
        // InnerJoin_F();

        // InnerJoinMultiField_Q();
        // InnerJoinMultiField_F();

        // GroupJoin_Q();
        // GroupJoin_F();
        
        // LeftOuterJoin_Q();
        LeftOuterJoin_F();
    }

    #region 1. INNER JOIN

    /// <summary>
    /// Inner join data, query syntax
    /// </summary>
    void InnerJoin_Q()
    {
        var allMovies = Repository.GetAllMovies();
        var castMembers = Repository.GetSomeCastMembers();

        var allCast =
            from movie in allMovies
            join castMember in castMembers
                on movie.Name equals castMember.Movie
            select (Movie: movie.Name, Released: movie.ReleaseDate, Actor: castMember.Actor);

        PrintAll(allCast);
    }

    /// <summary>
    /// Inner join data, fluent syntax
    /// </summary>
    void InnerJoin_F()
    {
        var allMovies = Repository.GetAllMovies();
        var castMembers = Repository.GetSomeCastMembers();

        var allCast = allMovies.Join(
            castMembers, // inner collection
            movie => movie.Name, // key selector for outer collection
            castMember => castMember.Movie, // key selector for inner collection 
            (movie, castMember) =>
                (Movie: movie.Name, Released: movie.ReleaseDate, Actor: castMember.Actor));

        PrintAll(allCast);
    }

    #endregion

    #region 2. INNER JOIN DATA ON MULTIPLE PROPERTIES

    /// <summary>
    /// Inner join data on multiple properties, query syntax
    /// </summary>
    void InnerJoinMultiField_Q()
    {
        var movieDirectors = Repository.GetAllMovies()
            .SelectMany(
                movie => movie.Directors,
                (movie, director) => (Movie: movie, Director: director));
        var directors = Repository.GetSomeDirectors();

        var result =
            from movieDirector in movieDirectors
            join director in directors
                on new { movieDirector.Director.FirstName, movieDirector.Director.LastName }
                equals new { director.FirstName, director.LastName }
            select (Movie: movieDirector.Movie.Name, Director: director.FullName);

        PrintAll(result);
    }

    /// <summary>
    /// Inner join data on multiple properties, fluent syntax
    /// </summary>
    void InnerJoinMultiField_F()
    {
        var movieDirectors = Repository.GetAllMovies()
            .SelectMany(
                movie => movie.Directors,
                (movie, director) => (Movie: movie, Director: director));
        var directors = Repository.GetSomeDirectors();

        var result = movieDirectors.Join(
            directors,
            movieDirector => new { movieDirector.Director.FirstName, movieDirector.Director.LastName },
            director => new { director.FirstName, director.LastName },
            (movieDirector, director) => (Movie: movieDirector.Movie.Name, Director: director.FullName));

        PrintAll(result);
    }

    #endregion

    #region 3. GROUP JOIN DATA

    /// <summary>
    /// Group join data, query syntax
    /// </summary>
    void GroupJoin_Q()
    {
        var allMovies = Repository.GetAllMovies();
        var castMembers = Repository.GetSomeCastMembers();

        var allCast =
            from movie in allMovies
            join castMember in castMembers
                on movie.Name
                equals castMember.Movie
                into cast
            select (Movie: movie.Name, Actors: cast);

        foreach (var movieWithCast in allCast)
        {
            Console.WriteLine($"{movieWithCast.Movie} ({movieWithCast.Actors.Count()})");
        }
    }

    /// <summary>
    /// Group join data, fluent syntax
    /// </summary>
    void GroupJoin_F()
    {
        var allMovies = Repository.GetAllMovies();
        var castMembers = Repository.GetSomeCastMembers();

        var allCast = allMovies.GroupJoin(
            castMembers,
            movie => movie.Name,
            castMember => castMember.Movie,
            (movie, cast) => // Func<TOuter, IEnumerable<TInner>, TResult>
                (Movie: movie.Name, Actors: cast));

        foreach (var movieWithCast in allCast)
        {
            Console.WriteLine($"{movieWithCast.Movie} ({movieWithCast.Actors.Count()})");
        }
    }

    #endregion

    #region 4. LEFT OUTER JOIN DATA

    /// <summary>
    /// Left outer join data, query syntax
    /// </summary>
    void LeftOuterJoin_Q()
    {
        var allMovies = Repository.GetAllMovies();
        var castMembers = Repository.GetSomeCastMembers();

        var allCast =
            from movie in allMovies
            join castMember in castMembers
                on movie.Name
                equals castMember.Movie
                into cast // first apply a group join
            from actor in cast.DefaultIfEmpty() // then apply a SelectMany() with DefaultIfEmpty(), note: nested from clause => SelectMany()
            select (Movie: movie.Name, Actor: actor);

        foreach (var movieWithCast in allCast)
        {
            Console.WriteLine($"{movieWithCast.Movie} ({movieWithCast.Actor?.Actor})");
        }
    }

    /// <summary>
    /// Left outer join data, fluent syntax
    /// </summary>
    void LeftOuterJoin_F()
    {
        var allMovies = Repository.GetAllMovies();
        var castMembers = Repository.GetSomeCastMembers();

        var allCast = allMovies
            .GroupJoin(
                castMembers,
                movie => movie.Name,
                castMember => castMember.Movie,
                (movie, cast) =>
                    (Movie: movie.Name, Actors: cast))
            .SelectMany(
                movieWithActors => movieWithActors.Actors.DefaultIfEmpty(),
                (movieWithActors, actor) =>
                    (Movie: movieWithActors.Movie, Actor: actor));

        foreach (var movieWithCast in allCast)
        {
            Console.WriteLine($"{movieWithCast.Movie} ({movieWithCast.Actor?.Actor})");
        }
    }

    #endregion
}