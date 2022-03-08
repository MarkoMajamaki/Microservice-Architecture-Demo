using MediatR;

namespace Identity.Application;

public record LoginExternalCommand(string Token, IExternalAuthenticationService ExternalAuthenticationService) : IRequest<LoginExternalCommandResponse>;
