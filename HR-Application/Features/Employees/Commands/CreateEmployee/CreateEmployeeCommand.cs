using HR_Application.Features.Employees.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Employees.Commands.CreateEmployee
{
    public record CreateEmployeeCommand(CreateEmployeeDto dto):IRequest<EmployeeResponseDto>;
    
}
