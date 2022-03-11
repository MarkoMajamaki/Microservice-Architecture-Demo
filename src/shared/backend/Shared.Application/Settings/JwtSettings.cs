namespace Shared.Application;

public class JwtSettings
{
    public const string Key = "JWT";
    public string ValidAudience { get; init; }
    public string ValidIssuer { get; init; }
    public string Secret { get; init; }
}