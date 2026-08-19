
using HR_Application.Features.GeneralSettings.Commands.UpdateGeneralSettings;
using HR_Application.Features.GeneralSettings.DTOs;
using HR_Application.Features.GeneralSettings.Queries.GetGeneralSettings;
using MediatR;

namespace HR__Management_System.EndPoints.GeneralSettings
{
    public static class GeneralSettingsEndpoint
    {
        public static void MapGeneralSettingsEndpoint(this IEndpointRouteBuilder endpoints)
        {
            var settingsGroup = endpoints.MapGroup("api/general-settings")
                .WithTags("General Settings")
                .RequireAuthorization(policy => policy.RequireRole("Admin", "HR"));

            
            settingsGroup.MapGet("/", async (ISender mediator) =>
            {
                var result = await mediator.Send(new GetGeneralSettingsQuery());
                return Results.Ok(result);
            });

            
            settingsGroup.MapPut("/", async (UpdateGeneralSettingsDto dto, ISender mediator) =>
            {
                var result = await mediator.Send(new UpdateGeneralSettingsCommand(dto));
                if (result)
                {
                    return Results.Ok(new { Success = true, Message = "General Settings saved successfully." });
                }
                return Results.BadRequest("Failed to save settings.");
            });
        }
    }
}
