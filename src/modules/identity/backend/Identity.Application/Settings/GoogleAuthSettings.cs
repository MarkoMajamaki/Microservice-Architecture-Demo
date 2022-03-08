namespace Identity.Application;

public class GoogleAuthSettings
{
    public const string Key = "Google";
    public string ClientId { get; init; }
    public string ClientSecret { get; init; }
}