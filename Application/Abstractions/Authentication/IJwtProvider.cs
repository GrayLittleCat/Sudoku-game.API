using SharedKernel;

namespace Application.Abstractions.Authentication;

public interface IJwtProvider
{
    Task<Result<string>> GetForCredentialsAsync(string email, string password);
}
