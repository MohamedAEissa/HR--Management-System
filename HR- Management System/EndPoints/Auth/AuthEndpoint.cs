using HR_Application.Features.Auth.Commands.CreateAccount;
using HR_Application.Features.Auth.Commands.LoginAccount;
using HR_Application.Features.Auth.DTOs;
using MediatR;

namespace HR__Management_System.EndPoints.Auth
{
    public static class AuthEndpoint
    {
        public static void MapAuthEndpoint(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("api/auth/create-account", async (CreateAccountDto dto, IMediator mediator) =>
            {
                var command = new CreateAccountCommand(dto);
                var result = await mediator.Send(command);

                if (result)
                {
                    return Results.Ok(new { Success = true, Message = "Account created and role assigned successfully." });
                }

                return Results.BadRequest("Failed to create account.");
            })
            .WithName("CreateAccount")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithTags("Auth");

            endpoints.MapPost("api/auth/login", async (LoginDto dto, IMediator mediator) =>
            {
                var command = new LoginAccountCommand(dto);
                var result = await mediator.Send(command);

                return Results.Ok(new
                {
                    Success = true,
                    Message = "Logged in successfully.",
                    Data = result
                });
            })
            .WithName("Login")
            .Produces<AuthResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithTags("Auth");


        }
    }
}
