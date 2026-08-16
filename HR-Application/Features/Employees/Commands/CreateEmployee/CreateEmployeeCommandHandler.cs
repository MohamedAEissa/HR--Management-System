using AutoMapper;
using HR_Application.Features.Employees.DTOs;
using HR_Application.Interfaces.Persistence;
using HR_Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, EmployeeResponseDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateEmployeeCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<EmployeeResponseDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var department = await _context.Departments
              .FirstOrDefaultAsync(d => d.Id == request.dto.DepartmentId, cancellationToken);

            if (department == null)
                throw new KeyNotFoundException($"Department with ID '{request.dto.DepartmentId}' was not found.");

            var employee = _mapper.Map<Employee>(request.dto);

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync(cancellationToken);

            var createdEmployee = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == employee.Id, cancellationToken);

            var responseDto = _mapper.Map<EmployeeResponseDto>(createdEmployee);
           

            return responseDto;

        }
    }
}
