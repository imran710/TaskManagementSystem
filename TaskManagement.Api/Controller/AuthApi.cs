using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Api.Common;
using TaskManagement.Api.Common.Response;
using TaskManagement.Application.Auth.Command;

namespace TaskManagement.Api.Controller;

public class AuthApi : IEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/auth").WithTags("Auth").WithOpenApi(); ;
        group.MapPost("/v1/email/login", EmailLogin) 
         .WithSummary("Login with email");
    }
    private static async Task<Results<Ok<ApiResponse<EmailLoginResponse>>,JsonHttpResult<ApiResponse<EmailLoginResponse>>>> EmailLogin(
          [FromBody] EmailLoginRequest request,
          [FromServices] EmailLoginHandler emailLoginHandler,
          HttpContext httpContext,
          CancellationToken cancellationToken = default)
    {
        var result = await emailLoginHandler.Execute(request, httpContext, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
            return ApiResponseResult.Problem(result);

        return ApiResponseResult.Success(result);
    }
}
