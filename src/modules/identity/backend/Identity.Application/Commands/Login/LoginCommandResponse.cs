namespace Identity.Application;

public record LoginCommandResponse(string UserId, string AccessToken);