using Shared.Domain;

namespace Order.Domain;

public class Customer : Entity, IAggregateRoot
{    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Address Address { get; private set; }

    private Customer()
    {
        // required by EF
    }

    public Customer(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public Customer(string firstName, string lastName, string country, string city, string postalCode, string street)
    {
        FirstName = firstName;
        LastName = lastName;
        Address = new Address(country, city, postalCode, street);
    }

    public void UpdateAddress(string country, string city, string postalCode, string street)
    {
        Address = new Address(country, city, postalCode, street);
    }
}