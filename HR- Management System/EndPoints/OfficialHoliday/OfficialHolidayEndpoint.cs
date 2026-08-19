using HR_Application.Features.OfficialHoliday.Commands.CreateOfficialHoliday;
using HR_Application.Features.OfficialHoliday.Commands.DeleteOfficialHoliday;
using HR_Application.Features.OfficialHoliday.DTOs;
using HR_Application.Features.OfficialHoliday.Queries.GetOfficialHolidays;
using MediatR;

namespace HR__Management_System.EndPoints.OfficialHoliday
{
    public static class OfficialHolidayEndpoint
    {
        public static void MapOfficialHolidayEndpoint(this IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("api/official-holidays")
                .WithTags("Official Holidays")
                .RequireAuthorization(policy => policy.RequireRole("Admin", "HR"));

           
            group.MapGet("/", async (ISender mediator) =>
            {
                var result = await mediator.Send(new GetOfficialHolidaysQuery());
                return Results.Ok(result);
            });

          
            group.MapPost("/", async (CreateOfficialHolidayDto dto, ISender mediator) =>
            {
                var holidayId = await mediator.Send(new CreateOfficialHolidayCommand(dto));
                return Results.Created($"/api/official-holidays/{holidayId}", new { Id = holidayId, Message = "Holiday created successfully." });
            });

            
            group.MapGroup("api/official-holidays").MapDelete("/{id:guid}", async (Guid id, ISender mediator) =>
            {
                var result = await mediator.Send(new DeleteOfficialHolidayCommand(id));
                return result ? Results.Ok(new { Message = "Holiday deleted successfully." }) : Results.NotFound();
            });
        }
    }
}
