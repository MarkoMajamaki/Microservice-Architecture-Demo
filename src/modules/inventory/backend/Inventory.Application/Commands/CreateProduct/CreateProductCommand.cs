using MediatR;

namespace Inventory.Application;

public class CreateProductCommand : IRequest<CreateProductCommandResponse>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public Byte[] Image { get; set; }
}