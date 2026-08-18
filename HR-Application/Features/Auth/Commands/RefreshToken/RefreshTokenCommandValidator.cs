using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.dto.AccessToken)
                .NotEmpty().WithMessage("Access Token is required.");

            RuleFor(x => x.dto.RefreshToken)
                .NotEmpty().WithMessage("Refresh Token is required.");
        }
    }
}
