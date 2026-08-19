using HR_Application.Features.PayrollReport.Commands.CreatePayroll;
using HR_Application.Features.PayrollReport.Commands.DeleteSalaryReport;
using HR_Application.Features.PayrollReport.DTOs;
using HR_Application.Features.PayrollReport.Queries.GetSalaryReportById;
using HR_Application.Features.PayrollReport.Queries.GetSalaryReports;
using HR_Application.Features.Payrolls.Commands.CreatePayroll;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace HR_Management_System.Endpoints
{
    public static class SalaryReportEndpoints
    {
        public static void MapSalaryReportEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("api/salary-reports")
                .WithTags("Salary Reports Management")
                .RequireAuthorization(policy => policy.RequireRole("Admin", "HR"));

            group.MapGet("/", async (Guid? employeeId, int? month, int? year, ISender mediator) =>
            {
                var query = new GetSalaryReportsQuery(employeeId, month, year);
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            
            group.MapGet("/{id:guid}", async (Guid id, ISender mediator) =>
            {
                var query = new GetSalaryReportByIdQuery(id);
                var result = await mediator.Send(query);
                return result != null ? Results.Ok(result) : Results.NotFound();
            });

           
            group.MapPost("/", async (CreatePayrollDto dto, ISender mediator) =>
            {
                var id = await mediator.Send(new CreatePayrollCommand(dto));
                return Results.Created($"/api/salary-reports/{id}", new { Id = id, Message = "Salary report generated successfully." });
            });

          
            group.MapDelete("/{id:guid}", async (Guid id, ISender mediator) =>
            {
                var result = await mediator.Send(new DeleteSalaryReportCommand(id));
                return result ? Results.Ok(new { Message = "Salary report deleted successfully." }) : Results.NotFound();
            });
        }
    }
}