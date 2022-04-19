using MediatR;

namespace Basket.Application;

public class AddItemToBasketCommandHandler : IRequestHandler<AddItemToBasketCommand, AddItemToBasketCommandResponse>
{
    public Task<AddItemToBasketCommandResponse> Handle(AddItemToBasketCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}