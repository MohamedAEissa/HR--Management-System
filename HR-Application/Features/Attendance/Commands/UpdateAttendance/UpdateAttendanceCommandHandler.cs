using HR_Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Attendance.Commands.UpdateAttendance
{
    public class UpdateAttendanceCommandHandler : IRequestHandler<UpdateAttendanceCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public UpdateAttendanceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateAttendanceCommand request, CancellationToken cancellationToken)
        {
            var attendance = await _context.Attendances.FindAsync(new object[] { request.Id }, cancellationToken);
            if (attendance == null) return false;

            attendance.Date = Convert.ToDateTime(request.Dto.Date).Date;
            attendance.CheckInTime = request.Dto.CheckInTime;
            attendance.CheckOutTime = request.Dto.CheckOutTime;
            attendance.OvertimeHours = request.Dto.OvertimeHours;
            attendance.DeductionHours = request.Dto.DeductionHours;
            attendance.Status = request.Dto.Status;


            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
