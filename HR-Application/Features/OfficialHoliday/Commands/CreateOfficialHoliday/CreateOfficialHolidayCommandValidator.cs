using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.OfficialHoliday.Commands.CreateOfficialHoliday
{
    public class CreateOfficialHolidayCommandValidator : AbstractValidator<CreateOfficialHolidayCommand>
    {
        public CreateOfficialHolidayCommandValidator()
        {
            RuleFor(x => x.Dto.Name)
                .NotEmpty().WithMessage("Holiday name is required.")
                .MaximumLength(100).WithMessage("Holiday name cannot exceed 100 characters.");

            RuleFor(x => x.Dto.Date)
                 .NotEmpty().WithMessage("Holiday date is required.")
                 .Must(date => date != default(DateTime))
                 .WithMessage("Please enter a valid date format (YYYY-MM-DD).");
        }
    }
}
