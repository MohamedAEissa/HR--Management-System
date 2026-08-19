using HR_Application.Features.PayrollReport.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.PayrollReport.Commands.CreatePayroll
{
    public record CreatePayrollCommand(CreatePayrollDto Dto) : IRequest<Guid>;
}
