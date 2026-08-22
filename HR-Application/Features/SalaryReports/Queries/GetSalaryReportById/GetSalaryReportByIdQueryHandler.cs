using HR_Application.Features.SalaryReports.DTOs;
using HR_Application.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.SalaryReports.Queries.GetSalaryReportById
{
    public class GetSalaryReportByIdQueryHandler : IRequestHandler<GetSalaryReportByIdQuery, SalaryReporResponsetDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetSalaryReportByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SalaryReporResponsetDto?> Handle(GetSalaryReportByIdQuery request, CancellationToken cancellationToken)
        {
            var s = await _context.SalaryReports
                .Include(s => s.Employee)
                .ThenInclude(e => e.Department)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (s == null) return null;

            return new SalaryReporResponsetDto
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
            };
        }
    }
}
