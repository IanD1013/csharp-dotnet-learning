using FluentValidation;
using FluentValidation.Results;
using Library.Api.Endpoints.Internal;
using Library.Api.Models;
using Library.Api.Services;

namespace Library.Api.Endpoints;

public class LibraryEndpoints : IEndpoints
{
    private const string ContextType = "application/json";
    private const string Tag = "Books";
    private const string BaseRoute = "books";
    
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost(BaseRoute, CreateBookAsync)
            .WithName("CreateBook")
            .Accepts<Book>(ContextType)
            .Produces<Book>(StatusCodes.Status201Created)
            .Produces<IEnumerable<ValidationFailure>>(StatusCodes.Status400BadRequest)
            .WithTags(Tag);


        app.MapGet(BaseRoute, GetAllBooksAsync)
            .WithName("GetBooks")
            .Produces<IEnumerable<Book>>(StatusCodes.Status200OK)
            .WithTags(Tag);


        app.MapGet($"{BaseRoute}/{{isbn}}", GetBookByIsbnAsync)
            .WithName("GetBook")
            .Produces<Book>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(Tag);

        
        app.MapPut($"{BaseRoute}/{{isbn}}", UpdateBookAsync)
            .WithName("UpdateBook")
            .Accepts<Book>(ContextType)
            .Produces<Book>(StatusCodes.Status200OK)
            .Produces<IEnumerable<ValidationFailure>>(StatusCodes.Status400BadRequest)
            .WithTags(Tag);

        
        app.MapDelete($"{BaseRoute}/{{isbn}}", DeleteBookAsync)
            .WithName("DeleteBook")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(Tag);

        
        app.MapGet("status", () => Results.Extensions.Html("""
                                                           <!doctype html>
                                                           <html>
                                                               <head><title>Status page</title></head>
                                                               <body>
                                                                   <h1>Status</h1>
                                                                   <p>The server is working fine. Bye bye!</p>
                                                               </body>
                                                           </html>
                                                           """))
            .ExcludeFromDescription()
            .RequireCors("AnyOrigin");
    }

    private static async Task<IResult> CreateBookAsync(Book book, IBookService bookService, IValidator<Book> validator)
    {
        var validationResult = await validator.ValidateAsync(book);
        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }

        var created = await bookService.CreateAsync(book);
        if (!created)
        {
            return Results.BadRequest(new List<ValidationFailure>
            {
                new("Isbn", "A book with this ISBN-13 already exists")
            });
        }

        return Results.CreatedAtRoute("GetBook", new { isbn = book.Isbn }, book);
    }

    private static async Task<IResult> GetAllBooksAsync(IBookService bookService, string? searchTerm)
    {
        if (searchTerm is not null && !string.IsNullOrWhiteSpace(searchTerm))
        {
            var matchedBooks = await bookService.SearchByTitleAsync(searchTerm);
            return Results.Ok(matchedBooks);
        }

        var books = await bookService.GetAllAsync();
        return Results.Ok(books);
    }

    private static async Task<IResult> GetBookByIsbnAsync(string isbn, IBookService bookService)
    {
        var book = await bookService.GetByIsbnAsync(isbn);
        return book is not null ? Results.Ok(book) : Results.NotFound();
    }

    private static async Task<IResult> UpdateBookAsync(string isbn, Book book, IBookService bookService,
        IValidator<Book> validator)
    {
        book.Isbn = isbn;
        var validationResult = await validator.ValidateAsync(book);
        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }

        var updated = await bookService.UpdateAsync(book);
        return updated ? Results.Ok(book) : Results.NotFound();
    }
    
    private static async Task<IResult> DeleteBookAsync(string isbn, IBookService bookService)
    {
        var deleted = await bookService.DeleteAsync(isbn);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IBookService, BookService>();
    }
}