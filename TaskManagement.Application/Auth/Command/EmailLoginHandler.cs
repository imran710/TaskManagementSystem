using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Owin.Security.Infrastructure;
using Shared.Liberary;
using Shared.Library.Results;
using TaskManagement.Application.Helper;
using TaskManagement.Domain.Common;
using TaskManagement.Domain.Helper;
using TaskManagement.Domain.Model;
using TaskManagement.Infrastructure.option;
using TaskManagement.Infrastructure.Persistence;
using SecurityHelper = TaskManagement.Domain.Helper.SecurityHelper;

namespace TaskManagement.Application.Auth.Command;
public class EmailLoginHandler(
    AppDbContext appDbContext,
    SecurityHelper securityHelper,
    IJwtTokenHelper jwtTokenHelper,
    IOptionsMonitor<DomainOption.AuthOption> authOptions
) : BaseHandler<EmailLoginRequest, EmailLoginResponse>
{
    protected override async Task<Result<EmailLoginResponse>> Handle(
        EmailLoginRequest request,
        CancellationToken cancellationToken = default)
    {
        // Find the user by email
        var user = await appDbContext.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user is null)
        {
            await Task.Delay(50, cancellationToken); // Prevent timing attacks
            return EmailLoginErrors.InvalidCredentials;
        }

        // Verify the password
        if (!request.Password.Equals(EncryptoEngine.Decrypt(user.PasswordHash, "sT9y3X7kfE&ZDg6q")))
        {
            await Task.Delay(50, cancellationToken);
            return EmailLoginErrors.InvalidCredentials;
        }

        // Generate the claims
        var claims = UserClaimGenerator.GetUserClaims(user.Id, user.FullName);

        // Generate access token
        var accessToken = jwtTokenHelper.GenerateAccessToken(claims);
         
        await appDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);



        var userModel = new UserModel
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role.ToString()
        };
        // Return response
        return new EmailLoginResponse(
            userModel,
            new TokenInfoModel(
                accessToken,
                jwtTokenHelper.JwtOption.AccessToken.ExpireInMinutes
            )
        );
    }
}
