using HR_Application.Features.SalaryReports.DTOs;
using HR_Application.Interfaces.Persistence;
using HR_Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.SalaryReports.Commands.UpdateSalaryReport
{
    public class UpdateSalaryReportCommandHandler : IRequestHandler<UpdateSalaryReportCommand, SalaryReporResponsetDto>
    {
        private readonly IApplicationDbContext _context;

        public UpdateSalaryReportCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SalaryReporResponsetDto> Handle(UpdateSalaryReportCommand request, CancellationToken cancellationToken)
        {
            var salaryReport = await _context.SalaryReports
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (salaryReport == null) throw new Exception("Salary report not found.");

            var dto = request.Dto;

            var employee = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == dto.EmployeeId, cancellationToken);

            if (employee == null) throw new Exception("Employee not found.");

            var attendances = await _context.Attendances
                .Where(a => a.EmployeeId == dto.EmployeeId &&
                            a.Date.Month == dto.Month &&
                            a.Date.Year == dto.Year)
                .ToListAsync(cancellationToken);

            int attendanceDays = attendances.Count(a => a.Status == AttendanceStatus.Present || a.Status == AttendanceStatus.late);
            int absenceDays = attendances.Count(a => a.Status == AttendanceStatus.Absent);

            decimal totalOvertimeHours = attendances.Sum(a => a.OvertimeHours);
            decimal totalDeductionHours = attendances.Sum(a => a.DeductionHours);

            decimal hourlyRate = employee.Salary > 0 ? (employee.Salary / 30m / 8m) : 0;
            decimal dailyRate = employee.Salary > 0 ? (employee.Salary / 30m) : 0;

            decimal totalOvertimeAmount = totalOvertimeHours * hourlyRate * 1.5m;

            // حساب الخصومات الجديدة (التأخير + الغياب)
            decimal delayDeductionAmount = totalDeductionHours * hourlyRate;
            decimal absenceDeductionAmount = absenceDays * dailyRate;
            decimal totalDeductionAmount = delayDeductionAmount + absenceDeductionAmount;

            decimal netSalary = employee.Salary + totalOvertimeAmount - totalDeductionAmount;

            // تحديث الحقول في الـ Report القديم
            salaryReport.EmployeeId = dto.EmployeeId;
            salaryReport.Month = dto.Month;
            salaryReport.Year = dto.Year;
            salaryReport.BasicSalary = employee.Salary;
            salaryReport.AttendanceDays = attendanceDays;
            salaryReport.AbsenceDays = absenceDays;
            salaryReport.TotalOvertimeHours = totalOvertimeHours;
            salaryReport.TotalDeductionHours = totalDeductionHours;
            salaryReport.TotalOvertimeAmount = Math.Round(totalOvertimeAmount, 2);
            salaryReport.TotalDeductionAmount = Math.Round(totalDeductionAmount, 2);
            salaryReport.NetSalary = Math.Round(netSalary, 2);

            _context.SalaryReports.Update(salaryReport);
            await _context.SaveChangesAsync(cancellationToken);

            return new SalaryReporResponsetDto
            {
                Id = salaryReport.Id,
                EmployeeId = employee.Id,
                EmployeeName = employee.FullName,
                DepartmentName = employee.Department?.Name ?? "No Department",
                Month = salaryReport.Month,
                Year = salaryReport.Year,
                BasicSalary = salaryReport.BasicSalary,
                AttendanceDays = salaryReport.AttendanceDays,
                AbsenceDays = salaryReport.AbsenceDays,
                TotalOvertimeHours = salaryReport.TotalOvertimeHours,
                TotalDeductionHours = salaryReport.TotalDeductionHours,
                TotalOvertimeAmount = salaryReport.TotalOvertimeAmount,
                TotalDeductionAmount = salaryReport.TotalDeductionAmount,
                NetSalary = salaryReport.NetSalary
            };
        }
    }
}