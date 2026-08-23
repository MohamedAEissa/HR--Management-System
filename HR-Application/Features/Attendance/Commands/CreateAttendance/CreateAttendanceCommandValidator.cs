using FluentValidation;
using HR_Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Attendance.Commands.CreateAttendance
{
    public class CreateAttendanceCommandValidator : AbstractValidator<CreateAttendanceCommand>
    {
        public CreateAttendanceCommandValidator(IApplicationDbContext context)
        {

            RuleFor(x => x.Dto.EmployeeId)
                .NotEmpty().WithMessage("Employee Id is required.");    

            RuleFor(x => x.Dto.Date)
                .NotEmpty().WithMessage("Date is required.")
                .Must(dateStr => DateTime.TryParse(dateStr, out _))
                .WithMessage("Invalid date format. Please use YYYY-MM-DD.")
                .MustAsync(async (command, dateStr, cancellation) =>
                {
                   
                    if (!DateTime.TryParse(dateStr, out DateTime parsedDate)) return false;

                    var employeeId = command.Dto.EmployeeId;

                    
                    bool exists = await context.Attendances
                        .AnyAsync(a => a.EmployeeId == employeeId && a.Date.Date == parsedDate.Date, cancellation);

                   
                    return !exists;
                })
                .WithMessage("An attendance record for this employee already exists on this date.");
        }
    }
}
