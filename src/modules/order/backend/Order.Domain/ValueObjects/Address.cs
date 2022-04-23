using Shared.Domain;

namespace Order.Domain;

public class Address : ValueObject
{
    public string Country { get; private set; }
    public string City { get; private set; }
    public string ZipCode { get; private set; }  
    public string Street { get; private set; }

    public Address(string country, string city, string zipCode, string street)
    {
        Country = country;
        City = city;
        ZipCode = zipCode;
        Street = street;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Country;
        yield return City;
        yield return ZipCode;
        yield return Street;
    }
}