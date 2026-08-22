using HR_Application.Interfaces.Persistence;
using HR_Domain.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HR_Application.Features.Attendance.Commands.CreateAttendance
{
    public class CreateAttendanceCommandHandler : IRequestHandler<CreateAttendanceCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateAttendanceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateAttendanceCommand request, CancellationToken cancellationToken)
        {
            var officialStartTime = new TimeSpan(8, 0, 0);
            var officialEndTime = new TimeSpan(16, 0, 0);

            decimal deductionHours = 0;
            decimal overtimeHours = 0;

           
            if (request.Dto.Status == AttendanceStatus.Absent)
            {
                request.Dto.CheckInTime = null;
                request.Dto.CheckOutTime = null;
            }
            else
            {
               
                if (request.Dto.CheckInTime.HasValue && request.Dto.CheckInTime.Value > officialStartTime)
                {
                    var delay = request.Dto.CheckInTime.Value - officialStartTime;
                    deductionHours = (decimal)delay.TotalHours;
                }

             
                if (request.Dto.CheckOutTime.HasValue && request.Dto.CheckOutTime.Value > officialEndTime)
                {
                    var over = request.Dto.CheckOutTime.Value - officialEndTime;
                    overtimeHours = (decimal)over.TotalHours;
                }
            }

            var attendance = new HR_Domain.Entities.Attendance
            {
                EmployeeId = request.Dto.EmployeeId,
                Date = Convert.ToDateTime(request.Dto.Date).Date,
                CheckInTime = request.Dto.CheckInTime,
                CheckOutTime = request.Dto.CheckOutTime,
                Status = request.Dto.Status,
                DeductionHours = Math.Round(deductionHours, 2),
                OvertimeHours = Math.Round(overtimeHours, 2)
            };

            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync(cancellationToken);

            return attendance.Id;
        }
    }
}