using HR_Application.Features.PayrollReport.Commands.CreatePayroll;
using HR_Application.Features.Payrolls.Commands.CreatePayroll;
using HR_Application.Interfaces;
using HR_Application.Interfaces.Persistence;
using HR_Domain.Entities;
using HR_Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HR_Application.Features.Payrolls.Commands.CreatePayroll
{
    public class CreateSalaryReportCommandHandler : IRequestHandler<CreatePayrollCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateSalaryReportCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreatePayrollCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == dto.EmployeeId, cancellationToken);

            if (employee == null)
            {
                throw new Exception("Employee not found.");
            }

           
            var attendances = await _context.Attendances
                .Where(a => a.EmployeeId == dto.EmployeeId &&
                            a.Date.Month == dto.Month &&
                            a.Date.Year == dto.Year)
                .ToListAsync(cancellationToken);

           
            int presentDays = attendances.Count(a => a.Status == AttendanceStatus.Present || a.Status == AttendanceStatus.late);
            int absentDays = attendances.Count(a => a.Status == AttendanceStatus.Absent);
            decimal totalOvertimeHours = attendances.Sum(a => a.OvertimeHours);
            decimal totalDeductionHours = attendances.Sum(a => a.DeductionHours);

            decimal hourlyRate = employee.Salary > 0 ? (employee.Salary / 30m / 8m) : 0;
            decimal overtimeAmount = totalOvertimeHours * hourlyRate * 1.5m;
            decimal deductionAmount = totalDeductionHours * hourlyRate;

           
            decimal netSalary = employee.Salary + overtimeAmount - deductionAmount;

            
            var salaryReport = new SalaryReport
            {
                EmployeeId = dto.EmployeeId,
                Month = dto.Month,
                Year = dto.Year,
                BasicSalary = employee.Salary,
                AttendanceDays = presentDays,
                AbsenceDays = absentDays,
                TotalOvertimeHours = totalOvertimeHours,
                TotalDeductionHours = totalDeductionHours,
                TotalOvertimeAmount = Math.Round(overtimeAmount, 2),
                TotalDeductionAmount = Math.Round(deductionAmount, 2),
                NetSalary = Math.Round(netSalary, 2)
            };

            _context.SalaryReports.Add(salaryReport); 
            await _context.SaveChangesAsync(cancellationToken);

            return salaryReport.Id; 
        }
    }
}