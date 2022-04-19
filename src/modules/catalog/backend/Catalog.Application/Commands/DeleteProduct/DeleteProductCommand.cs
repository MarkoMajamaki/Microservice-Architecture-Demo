using MediatR;

namespace Catalog.Application;

public class DeleteProductCommand : IRequest<DeleteProductCommandResponse>
{
    public int Id { get; set; }

    public DeleteProductCommand(int id)
    {
        Id = id;
    }
}