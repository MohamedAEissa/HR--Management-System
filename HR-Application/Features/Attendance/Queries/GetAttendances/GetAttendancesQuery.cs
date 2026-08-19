using HR_Application.Features.Attendance.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Attendance.Queries.GetAttendances
{
    public record GetAttendancesQuery(AttendanceFilterDto Filter) : IRequest<List<AttendanceDto>>;
}
