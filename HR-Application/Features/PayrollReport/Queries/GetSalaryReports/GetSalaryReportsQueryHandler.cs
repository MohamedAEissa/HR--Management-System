using HR_Application.Features.PayrollReport.DTOs;
using HR_Application.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.PayrollReport.Queries.GetSalaryReports
{
    public class GetSalaryReportsQueryHandler : IRequestHandler<GetSalaryReportsQuery, List<SalaryReportDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetSalaryReportsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SalaryReportDto>> Handle(GetSalaryReportsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.SalaryReports
                .Include(sr => sr.Employee)
                .ThenInclude(e => e.Department)
                .AsQueryable();

            
            if (request.EmployeeId.HasValue)
            {
                query = query.Where(sr => sr.EmployeeId == request.EmployeeId.Value);
            }

            if (request.Month.HasValue)
            {
                query = query.Where(sr => sr.Month == request.Month.Value);
            }

            if (request.Year.HasValue)
            {
                query = query.Where(sr => sr.Year == request.Year.Value);
            }

            return await query
                .Select(sr => new SalaryReportDto
                {
                    Id = sr.Id,
                    EmployeeId = sr.EmployeeId,
                    EmployeeName = sr.Employee.FullName,
                    DepartmentName = sr.Employee.Department != null ? sr.Employee.Department.Name : "No Dept",
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
                })
                .ToListAsync(cancellationToken);
        }
    }
    }
