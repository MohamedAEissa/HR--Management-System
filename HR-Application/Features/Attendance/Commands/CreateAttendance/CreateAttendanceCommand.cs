using HR_Application.Features.Attendance.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Attendance.Commands.CreateAttendance
{
    public record CreateAttendanceCommand(CreateAttendanceDto Dto) : IRequest<Guid>;
}
