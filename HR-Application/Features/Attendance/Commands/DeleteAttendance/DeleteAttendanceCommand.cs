using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Attendance.Commands.DeleteAttendance
{
    public record DeleteAttendanceCommand(Guid Id) : IRequest<bool>;
}
