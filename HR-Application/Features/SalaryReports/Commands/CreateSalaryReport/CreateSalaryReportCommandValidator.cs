using FluentValidation;
using HR_Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.SalaryReports.Commands.CreateSalaryReport
{
    public class CreateSalaryReportCommandValidator : AbstractValidator<CreateSalaryReportCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateSalaryReportCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Dto.EmployeeId)
                .NotEmpty().WithMessage("Employee ID is required.")
                .MustAsync(async (employeeId, cancellation) =>
                {
                    return await _context.Employees.AnyAsync(e => e.Id == employeeId, cancellation);
                }).WithMessage("The selected employee does not exist.");

            RuleFor(x => x.Dto.Month)
                .InclusiveBetween(1, 12).WithMessage("Month must be between 1 and 12.");

            RuleFor(x => x.Dto.Year)
                .GreaterThanOrEqualTo(2000).WithMessage("Invalid year.");

            RuleFor(x => x.Dto)
                .MustAsync(async (dto, cancellation) =>
                {
                    var exists = await _context.SalaryReports.AnyAsync(sr =>
                        sr.EmployeeId == dto.EmployeeId &&
                        sr.Month == dto.Month &&
                        sr.Year == dto.Year, cancellation);
                    return !exists;
                }).WithMessage("A salary report for this employee in this month and year already exists.");
        }
    }
}
