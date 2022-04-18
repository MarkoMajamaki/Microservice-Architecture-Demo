namespace Order.Domain;

public interface ICustomerRepository
{
    Task SaveAsync(Customer customer);
}