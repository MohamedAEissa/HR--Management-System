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

namespace HR_Application.Features.Departments.Queries
{
    public record GetDepartmentByIdQuery(Guid Id) : IRequest<DepartmentResponseDto>;

    public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, DepartmentResponseDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDepartmentByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DepartmentResponseDto> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

            if (department == null)
                throw new KeyNotFoundException($"Department with ID '{request.Id}' was not found.");

            return _mapper.Map<DepartmentResponseDto>(department);
        }
    }
}
