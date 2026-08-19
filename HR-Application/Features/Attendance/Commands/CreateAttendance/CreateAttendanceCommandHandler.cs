using HR_Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var attendance = new HR_Domain.Entities.Attendance
            {
                EmployeeId = request.Dto.EmployeeId,
                Date = Convert.ToDateTime(request.Dto.Date).Date,
                CheckInTime = request.Dto.CheckInTime,
                CheckOutTime = request.Dto.CheckOutTime,
                Status = request.Dto.Status,
               
            };

            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync(cancellationToken);

            return attendance.Id;
        }
    }
}
