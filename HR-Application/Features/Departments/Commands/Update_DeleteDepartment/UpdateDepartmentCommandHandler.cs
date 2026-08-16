using AutoMapper;
using HR_Application.Features.Departments.DTOs;
using HR_Application.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Departments.Commands.Update_DeleteDepartment
{
    public record UpdateDepartmentCommand(Guid Id, UpdateDepartmentDto Dto) : IRequest<DepartmentResponseDto>;
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, DepartmentResponseDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateDepartmentCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DepartmentResponseDto> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

            if (department == null)
                throw new KeyNotFoundException($"Department with ID '{request.Id}' was not found.");

            _mapper.Map(request.Dto, department);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<DepartmentResponseDto>(department);
        }
    }
}
