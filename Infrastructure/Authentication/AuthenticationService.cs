using Application.Abstractions.Authentication;
using FirebaseAdmin.Auth;
using SharedKernel;

namespace Infrastructure.Authentication;

internal sealed class AuthenticationService : IAuthenticationService
{
    public async Task<Result<string>> RegisterAsync(string email, string password)
    {
        var userArgs = new UserRecordArgs
        {
            Email = email,
            Password = password
        };

        try
        {
            var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArgs);

            return Result.Success(userRecord.Uid);
        }
        catch (FirebaseAuthException e)
        {
            return Result.Failure<string>(new Error("Authentication.CreateUserFailed",
                e.Message));
        }
        catch (ArgumentException e)
        {
            return Result.Failure<string>(new Error("Authentication.ArgumentException",
                e.Message));
        }
    }
}
