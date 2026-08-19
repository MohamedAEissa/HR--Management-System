using HR_Application.Features.PayrollReport.Commands.CreatePayroll;
using HR_Application.Features.PayrollReport.DTOs;
using HR_Application.Features.PayrollReport.Queries.GetSalaryReports;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.SalaryReportEndpoint
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

            
            group.MapPost("/", async (CreatePayrollDto dto, ISender mediator) =>
            {
                var id = await mediator.Send(new CreatePayrollCommand(dto));
                return Results.Created($"/api/salary-reports/{id}", new { Id = id, Message = "Salary report generated successfully." });
            });
        }
    }
}
