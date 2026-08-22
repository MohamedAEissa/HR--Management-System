
using HR_Application.Features.SalaryReports.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.SalaryReports.Commands.UpdateSalaryReport
{
    public record UpdateSalaryReportCommand(Guid Id, RequestSalaryReportDto Dto) : IRequest<SalaryReporResponsetDto>;
}
