using System.Net;

namespace Shared.Domain;

public class DomainException : Exception
{
    public string Title { get; init; }
    public HttpStatusCode StatusCode { get; set; }

    public DomainException(string message, HttpStatusCode statusCode, Exception innerException = null) : base(message, innerException)
    {
        StatusCode = statusCode;
    }

    public DomainException(string title, string message, HttpStatusCode statusCode, Exception innerException = null) : base(message, innerException)
    {
        Title = title;
        StatusCode = statusCode;
    }
}