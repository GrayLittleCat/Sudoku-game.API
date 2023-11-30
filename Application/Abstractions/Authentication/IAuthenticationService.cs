using SharedKernel;

namespace Application.Abstractions.Authentication;

public interface IAuthenticationService
{
    Task<Result<string>> RegisterAsync(string email, string password);
}
