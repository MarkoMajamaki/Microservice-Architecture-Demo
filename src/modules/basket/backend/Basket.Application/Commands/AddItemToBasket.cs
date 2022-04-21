using MediatR;

namespace Basket.Application;

public static class AddItemToBasket
{
    public record Command(int Id) : IRequest<Response>;

    public class Handler : IRequestHandler<Command, Response>
    {
        public Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public record Response();
}