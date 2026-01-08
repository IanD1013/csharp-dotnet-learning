using System.Text.Json;
using Movies.Api.Sdk;
using Refit;

var moviesApi = RestService.For<IMoviesApi>("https://localhost:5001");

var movie = await moviesApi.GetMoviesAsync("nick-the-greek-2022");

Console.WriteLine(JsonSerializer.Serialize(movie));