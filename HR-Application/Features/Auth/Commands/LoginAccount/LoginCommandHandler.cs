using AutoMapper;
using HR_Application.Features.Auth.DTOs;
using HR_Application.Interfaces.Identity;
using HR_Application.Interfaces.Persistence;
using HR_Domain.Common;
using HR_Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Auth.Commands.LoginAccount
{
    public class LoginCommandHandler : IRequestHandler<LoginAccountCommand, AuthResponseDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IApplicationDbContext _context;
        private readonly ITokenService _tokenService; 
        private readonly IMapper _mapper;
        public LoginCommandHandler(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IApplicationDbContext context,
            ITokenService tokenService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        public async Task<AuthResponseDto> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !user.IsActive)
            {
                throw new Exception("Invalid email or account is inactive.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password,false);
            if (!result.Succeeded)
            {
                throw new Exception("Invalid password.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "Employee";


            var employee = await _context.Employees
                    .Include(e => e.Department)
                    .FirstOrDefaultAsync(e => e.Id == user.EmployeeId, cancellationToken);

            var departmentName = employee?.Department.Name;

            var accessToken = _tokenService.CreateToken(user, roles.ToList());
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            var response = _mapper.Map<AuthResponseDto>(user);
            response.Role = role;
            response.DepartmentName = departmentName;
            response.AccessToken = accessToken;
            response.RefreshToken = refreshToken;
            response.RefreshTokenExpiration = user.RefreshTokenExpiryTime.Value;

            return response;
        }
    }
}
