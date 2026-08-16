using HR_Application.Features.Departments.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Departments.Commands.CreateDepartment
{
    public record CreateDepartmentCommand(CreateDepartmentDto Dto) : IRequest<DepartmentResponseDto>;
}

