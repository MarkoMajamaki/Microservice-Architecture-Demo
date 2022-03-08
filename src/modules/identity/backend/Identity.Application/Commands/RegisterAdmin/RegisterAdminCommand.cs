using MediatR;

namespace Identity.Application;

public record RegisterAdminCommand(string Email, string Password) : IRequest<bool>;
