using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Attendance.Commands.DeleteAttendance
{
    public class DeleteAttendanceCommandValidator : AbstractValidator<DeleteAttendanceCommand>
    {
        public DeleteAttendanceCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Attendance ID is required.");
        }
    }
}
