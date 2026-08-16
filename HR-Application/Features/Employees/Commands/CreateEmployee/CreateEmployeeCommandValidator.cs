using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandValidator: AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.dto.FullName)
                 .NotEmpty().WithMessage("Name is required.")
                 .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

            RuleFor(x => x.dto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.dto.Phone)
                .NotEmpty().WithMessage("Phone number is required.");

            RuleFor(x => x.dto.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than zero.");

            RuleFor(x => x.dto.DepartmentId)
                .NotEmpty().WithMessage("Department ID is required.");
        }
    }
}
