using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.PayrollReport.Commands.DeleteSalaryReport
{
    public record DeleteSalaryReportCommand(Guid Id) : IRequest<bool>;
}
