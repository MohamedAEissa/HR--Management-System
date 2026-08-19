using HR_Application.Features.Attendance.DTOs;
using HR_Application.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Attendance.Queries.GetAttendances
{
    public class GetAttendancesQueryHandler : IRequestHandler<GetAttendancesQuery, List<AttendanceDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAttendancesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AttendanceDto>> Handle(GetAttendancesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Attendances
                .Include(a => a.Employee)
                .ThenInclude(e => e.Department)
                .AsQueryable();

            var filter = request.Filter;

            if (filter.EmployeeId.HasValue)
            {
                query = query.Where(a => a.EmployeeId == filter.EmployeeId.Value);
            }
            if (!string.IsNullOrWhiteSpace(filter.DepartmentName))
            {
                query = query.Where(a => a.Employee.Department != null &&
                                         a.Employee.Department.Name.ToLower().Contains(filter.DepartmentName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.EmployeeName))
            {
                query = query.Where(a => a.Employee.FullName != null &&
                                         a.Employee.FullName.ToLower().Contains(filter.EmployeeName.ToLower()));
            }

            if (filter.DepartmentId.HasValue)
            {
                query = query.Where(a => a.Employee.DepartmentId == filter.DepartmentId.Value);
            }

            
            if (filter.FromDate.HasValue)
            {
                query = query.Where(a => a.Date >= filter.FromDate.Value.Date);
            }
            if (filter.ToDate.HasValue)
            {
                query = query.Where(a => a.Date <= filter.ToDate.Value.Date);
            }

            return await query
                .Select(a => new AttendanceDto
                {
                    Id = a.Id,
                    EmployeeId = a.EmployeeId,
                    EmployeeName = a.Employee.FullName,
                    DepartmentName = a.Employee.Department != null ? a.Employee.Department.Name : "No Dept",
                    Date = a.Date,
                    CheckInTime = a.CheckInTime,
                    CheckOutTime = a.CheckOutTime,
                    OvertimeHours = a.OvertimeHours,
                    DeductionHours = a.DeductionHours,
                    Status = a.Status,
                    
                })
                .OrderByDescending(a => a.Date)
                .ToListAsync(cancellationToken);

        }
    }
}
