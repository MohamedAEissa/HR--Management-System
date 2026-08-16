using HR_Application.Features.Departments.Commands.CreateDepartment;
using HR_Application.Features.Departments.Commands.Update_DeleteDepartment;
using HR_Application.Features.Departments.DTOs;
using HR_Application.Features.Departments.Queries;
using MediatR;

namespace HR__Management_System.EndPoints.DepartmentEndpoints
{
    public static class DepartmentEndpoints
    {
        public static void MapDepartmentEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/departments")
                           .WithTags("Departments");


            group.MapGet("/", async (IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(new GetAllDepartmentsQuery(), cancellationToken);
                return Results.Ok(result);
            })
            .Produces<IEnumerable<DepartmentResponseDto>>(StatusCodes.Status200OK);

            // 2. Get Department By Id
            group.MapGet("/{id:guid}", async (Guid id, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(new GetDepartmentByIdQuery(id), cancellationToken);
                return Results.Ok(result);
            })
            .Produces<DepartmentResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            // 3. Create Department
            group.MapPost("/", async (CreateDepartmentDto dto, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(new CreateDepartmentCommand(dto), cancellationToken);
                return Results.Ok(result);
            })
            .Produces<DepartmentResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

            // 4. Update Department
            group.MapPut("/{id:guid}", async (Guid id, UpdateDepartmentDto dto, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(new UpdateDepartmentCommand(id, dto), cancellationToken);
                return Results.Ok(result);
            })
            .Produces<DepartmentResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest);

            // 5. Delete Department
            group.MapDelete("/{id:guid}", async (Guid id, IMediator mediator, CancellationToken cancellationToken) =>
            {
                await mediator.Send(new DeleteDepartmentCommand(id), cancellationToken);
                return Results.Ok(new { message = "Department deleted successfully." });
            })
            .Produces(StatusCodes.Status200OK) 
             .Produces(StatusCodes.Status404NotFound);


        }
    }
}
