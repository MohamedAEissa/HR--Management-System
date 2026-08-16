using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
    {
       public CreateDepartmentCommandValidator() 
        {
            RuleFor(x => x.Dto.Name)
                .NotEmpty().WithMessage("Name Is Required")
                .MaximumLength(100).WithMessage("Max Lenght Of Name Is 100 Char");
        }
    }
}
