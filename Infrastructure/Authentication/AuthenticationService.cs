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
            return Result.Failure<string>(Error.Failure("Authentication.CreateUserFailed",
                e.Message));
        }
        catch (ArgumentException e)
        {
            return Result.Failure<string>(Error.Failure("Authentication.ArgumentException",
                e.Message));
        }
    }

    public async Task<Result<string>> DeleteAsync(string uid)
    {
        try
        {
            await FirebaseAuth.DefaultInstance.RevokeRefreshTokensAsync(uid);
            await FirebaseAuth.DefaultInstance.DeleteUserAsync(uid);
            return Result.Success(uid);
        }
        catch (FirebaseAuthException e)
        {
            return Result.Failure<string>(Error.Failure("Authentication.DeleteUserFailed", e.Message));
        }
        catch (ArgumentException e)
        {
            return Result.Failure<string>(Error.Failure("Authentication.ArgumentException", e.Message));
        }
    }

    public async Task<Result<string>> UpdateAsync(
        string uid,
        string? email = null,
        string? password = null,
        string? displayName = null)
    {
        var userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
        var userArgs = new UserRecordArgs
        {
            Uid = uid,
            Email = userRecord.Email,
            DisplayName = userRecord.DisplayName
        };
        if (email != null)
        {
            userArgs.Email = email;
        }

        if (password != null)
        {
            userArgs.Password = password;
        }

        if (displayName != null)
        {
            userArgs.DisplayName = displayName;
        }

        try
        {
            await FirebaseAuth.DefaultInstance.UpdateUserAsync(userArgs);
            return Result.Success(uid);
        }
        catch (FirebaseAuthException e)
        {
            return Result.Failure<string>(Error.Failure("Authentication.UpdateUserFailed", e.Message));
        }
        catch (ArgumentException e)
        {
            return Result.Failure<string>(Error.Failure("Authentication.ArgumentException", e.Message));
        }
    }
}
