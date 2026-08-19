using HR_Application.Features.PayrollReport.DTOs;
using HR_Application.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.PayrollReport.Queries.GetSalaryReportById
{
    public class GetSalaryReportByIdQueryHandler : IRequestHandler<GetSalaryReportByIdQuery, SalaryReportDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetSalaryReportByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SalaryReportDto?> Handle(GetSalaryReportByIdQuery request, CancellationToken cancellationToken)
        {
            var sr = await _context.SalaryReports
                 .Include(s => s.Employee)
                 .ThenInclude(e => e.Department)
                 .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (sr == null) return null;

            return new SalaryReportDto
            {
                Id = sr.Id,
                EmployeeId = sr.EmployeeId,
                EmployeeName = sr.Employee.FullName,
                DepartmentName = sr.Employee.Department?.Name ?? "No Dept",
                Month = sr.Month,
                Year = sr.Year,
                BasicSalary = sr.BasicSalary,
                AttendanceDays = sr.AttendanceDays,
                AbsenceDays = sr.AbsenceDays,
                TotalOvertimeHours = sr.TotalOvertimeHours,
                TotalDeductionHours = sr.TotalDeductionHours,
                TotalOvertimeAmount = sr.TotalOvertimeAmount,
                TotalDeductionAmount = sr.TotalDeductionAmount,
                NetSalary = sr.NetSalary
            };
    }
    }
}
