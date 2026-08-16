using AutoMapper;
using HR_Application.Features.Departments.DTOs;
using HR_Application.Interfaces.Persistence;
using HR_Application.Interfaces.Services;
using HR_Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand,DepartmentResponseDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public CreateDepartmentCommandHandler(IApplicationDbContext dbContext , ICurrentUserService currentUser,IMapper mapper)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
            _mapper = mapper;
        }
        async Task<DepartmentResponseDto> IRequestHandler<CreateDepartmentCommand, DepartmentResponseDto>.Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Department>(request.Dto);

            await _dbContext.Departments.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<DepartmentResponseDto>(entity);
        }
    }
}
