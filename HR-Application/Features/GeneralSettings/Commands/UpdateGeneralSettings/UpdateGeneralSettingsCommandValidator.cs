using FluentValidation;
using HR_Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.GeneralSettings.Commands.UpdateGeneralSettings
{
    public class UpdateGeneralSettingsCommandValidator : AbstractValidator<UpdateGeneralSettingsCommand>
    {
        public UpdateGeneralSettingsCommandValidator()
        {
            RuleFor(x => x.Dto.OvertimeHourRate)
                .GreaterThanOrEqualTo(0).WithMessage("Overtime rate cannot be negative.");

            RuleFor(x => x.Dto.DeductionHourRate)
                .GreaterThanOrEqualTo(0).WithMessage("Deduction rate cannot be negative.");

            RuleFor(x => x.Dto.WeeklyDaysOff)
                .NotEmpty().WithMessage("Weekly days off cannot be empty.")
               .Must(WeeklyDaysOff => WeeklyDaysOff == StaticWeeklyDaysOff.Saturday || WeeklyDaysOff == StaticWeeklyDaysOff.Sunday || WeeklyDaysOff == StaticWeeklyDaysOff.Monday || WeeklyDaysOff == StaticWeeklyDaysOff.Tuesday || WeeklyDaysOff == StaticWeeklyDaysOff.Wednesday || WeeklyDaysOff == StaticWeeklyDaysOff.Thursday || WeeklyDaysOff == StaticWeeklyDaysOff.Friday)
                .WithMessage("Invalid WeeklyDaysOff specified. Allowed WeeklyDaysOff are: Saturday, Sunday, Monday, Tuesday, Wednesday, Thursday, Friday"); ;


            //RuleFor(x => x.Dto.Role)
            //   .NotEmpty().WithMessage("Role is required.")
            //   .Must(role => role == Roles.Admin || role == Roles.HR || role == Roles.Employee)
            //   .WithMessage("Invalid role specified. Allowed roles are: Admin, HR, Employee.");
        }
    }
}
