using AutoMapper;
using HR_Application.Interfaces.Persistence;
using HR_Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Auth.Commands.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateAccountCommandHandler(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IApplicationDbContext context,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Email == dto.Email, cancellationToken);

            if (employee == null)
            {
                throw new Exception("Employee not found with this email.");
            }

            var roleExists = await _roleManager.RoleExistsAsync(dto.Role);
            if (!roleExists)
            {
                throw new Exception($"The role '{dto.Role}' does not exist in the system.");
            }

            var appUser = _mapper.Map<ApplicationUser>(dto);

            appUser.FullName = employee.FullName;
            appUser.EmployeeId = employee.Id;
            appUser.IsActive = true;
            appUser.CreatedAt = DateTime.UtcNow;

            var result = await _userManager.CreateAsync(appUser, dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to create user account: {errors}");
            }

            await _userManager.AddToRoleAsync(appUser, dto.Role);

            employee.UserId = appUser.Id;
            _context.Employees.Update(employee);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
