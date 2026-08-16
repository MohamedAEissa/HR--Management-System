using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandValidator:AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Employee ID is required.");

            RuleFor(x => x.Dto.FullName)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

            RuleFor(x => x.Dto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Dto.Phone)
                .NotEmpty().WithMessage("Phone number is required.");

            RuleFor(x => x.Dto.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than zero.");

            RuleFor(x => x.Dto.DepartmentId)
                .NotEmpty().WithMessage("Department ID is required.");
        }
    }
}
