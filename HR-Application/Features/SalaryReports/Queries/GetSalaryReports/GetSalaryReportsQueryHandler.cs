using HR_Application.Features.SalaryReports.DTOs;
using HR_Application.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.SalaryReports.Queries.GetSalaryReports
{
    public class GetSalaryReportsQueryHandler : IRequestHandler<GetSalaryReportsQuery, List<SalaryReporResponsetDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetSalaryReportsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SalaryReporResponsetDto>> Handle(GetSalaryReportsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.SalaryReports
                 .Include(s => s.Employee)
                 .ThenInclude(e => e.Department)
                 .AsQueryable();

            if (request.EmployeeId.HasValue)
            {
                query = query.Where(s => s.EmployeeId == request.EmployeeId.Value);
            }

            if (request.Month.HasValue)
            {
                query = query.Where(s => s.Month == request.Month.Value);
            }

            if (request.Year.HasValue)
            {
                query = query.Where(s => s.Year == request.Year.Value);
            }

            var reports = await query
                .Select(s => new SalaryReporResponsetDto
                {
                    Id = s.Id,
                    EmployeeId = s.EmployeeId,
                    EmployeeName = s.Employee.FullName,
                    DepartmentName = s.Employee.Department != null ? s.Employee.Department.Name : "No Department",
                    Month = s.Month,
                    Year = s.Year,
                    BasicSalary = s.BasicSalary,
                    AttendanceDays = s.AttendanceDays,
                    AbsenceDays = s.AbsenceDays,
                    TotalOvertimeHours = s.TotalOvertimeHours,
                    TotalDeductionHours = s.TotalDeductionHours,
                    TotalOvertimeAmount = s.TotalOvertimeAmount,
                    TotalDeductionAmount = s.TotalDeductionAmount,
                    NetSalary = s.NetSalary
                })
                .ToListAsync(cancellationToken);

            return reports;
        }
    }
}
