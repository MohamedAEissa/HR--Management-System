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
    public record GetAllDepartmentsQuery : IRequest<IEnumerable<DepartmentResponseDto>>;
    public class GetAllDepartmentsQueryHandler:IRequestHandler<GetAllDepartmentsQuery, IEnumerable<DepartmentResponseDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllDepartmentsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentResponseDto>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departments = await _context.Departments.ToListAsync(cancellationToken);
            return _mapper.Map<IEnumerable<DepartmentResponseDto>>(departments);
        }
    }
}
