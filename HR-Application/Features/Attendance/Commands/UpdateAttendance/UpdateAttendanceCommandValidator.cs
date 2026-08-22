using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Attendance.Commands.UpdateAttendance
{
    public class UpdateAttendanceCommandValidator : AbstractValidator<UpdateAttendanceCommand>
    {
        public UpdateAttendanceCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Attendance ID is required.");

            RuleFor(x => x.Dto.Date)
                .NotEmpty().WithMessage("Date is required.")
                .Must(dateStr => DateTime.TryParse(dateStr, out _))
                .WithMessage("Invalid date format. Please use YYYY-MM-DD.");

         
        }
    }
}
