using FastEndpoints;

namespace RiverBooks.Books.BookEndpoints;

internal class UpdatePrice(IBookService bookService) : Endpoint<UpdateBookPriceRequest, BookDto>
{
    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        Post("/books/{id}/pricehistory");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateBookPriceRequest req, CancellationToken ct)
    {
        // TODO: Handle not found
        await _bookService.UpdateBookPriceAsync(req.Id, req.newPrice);

        var updatedBook = await _bookService.GetBookByIdAsync(req.Id);

        await SendAsync(updatedBook);
    }
}