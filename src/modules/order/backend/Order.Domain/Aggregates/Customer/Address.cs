namespace Order.Domain;

public class Address
{
    public string Country { get; private set; }
    public string City { get; private set; }
    public string PostalCode { get; private set; }
    public string Street { get; private set; }
}