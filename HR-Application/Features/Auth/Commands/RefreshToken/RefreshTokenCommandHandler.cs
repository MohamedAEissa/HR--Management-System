using HR_Application.Features.Auth.DTOs;
using HR_Application.Interfaces.Identity;
using HR_Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponseDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public RefreshTokenCommandHandler(UserManager<ApplicationUser> userManager, ITokenService _tokenService)
        {
            _userManager = userManager;
            this._tokenService = _tokenService;
        }
        public async Task<AuthResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var accessToken = request.dto.AccessToken;
            var refreshToken = request.dto.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                throw new Exception("Invalid Access Token or Refresh Token");
            }

            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
               ?? principal.FindFirst("nameid")?.Value
               ?? principal.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                throw new Exception("Invalid Token Claims: User ID not found in token.");
            }

           
            var user = await _userManager.FindByIdAsync(userIdClaim);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new Exception("Invalid or Expired Refresh Token");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var newAccessToken = _tokenService.CreateToken(user, roles.ToList());
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return new AuthResponseDto
            {
                Id = user.Id.ToString(),
                Email = user.Email ?? string.Empty,
                FullName = user.FullName ?? string.Empty,
                Role = roles.FirstOrDefault() ?? string.Empty,
                DepartmentName = null,
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                RefreshTokenExpiration = user.RefreshTokenExpiryTime.Value
            };
        }
    }
}