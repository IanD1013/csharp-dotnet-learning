using Ardalis.Result;
using MediatR;

namespace RiverBooks.Users.UseCases;

public record AddItemToCartCommand(Guid BookId, int Quantity, string EmailAddress) : IRequest<Result>;

public class AddItemToCartHandler : IRequestHandler<AddItemToCartCommand, Result>
{
    public Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public interface IApplicationUserRepository
{
    Task<ApplicationUser> GetUserWithCartByEmailAsync(string email);
    Task SaveChangesAsync();
}