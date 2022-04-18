using MediatR;

namespace Inventory.Application;

public class UpdateProductCommand : IRequest<UpdateProductCommandResponse>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
}