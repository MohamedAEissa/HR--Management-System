using HR_Application.Features.PayrollReport.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.PayrollReport.Queries.GetSalaryReportById
{
    public record GetSalaryReportByIdQuery(Guid Id) : IRequest<SalaryReportDto?>;
}
