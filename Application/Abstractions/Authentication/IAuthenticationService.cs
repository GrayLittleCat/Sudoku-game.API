using SharedKernel;

namespace Application.Abstractions.Authentication;

public interface IAuthenticationService
{
    Task<Result<string>> RegisterAsync(string email, string password);
    Task<Result<string>> DeleteAsync(string uid);

    Task<Result<string>> UpdateAsync(
        string uid,
        string? email = null,
        string? password = null,
        string? displayName = null);
}
