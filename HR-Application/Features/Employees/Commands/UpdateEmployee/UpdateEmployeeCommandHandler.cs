using AutoMapper;
using HR_Application.Features.Employees.DTOs;
using HR_Application.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeResponseDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<EmployeeResponseDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.Include(d=>d.Department)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (employee == null)
                throw new KeyNotFoundException($"Employee with ID '{request.Id}' was not found.");

            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == request.Dto.DepartmentId, cancellationToken);

            if (department == null)
                throw new KeyNotFoundException($"Department with ID '{request.Dto.DepartmentId}' was not found.");

            _mapper.Map(request.Dto, employee);

            await _context.SaveChangesAsync(cancellationToken);

            

            var responseDto = _mapper.Map<EmployeeResponseDto>(employee);
           

            return responseDto;
        }
    }
}
