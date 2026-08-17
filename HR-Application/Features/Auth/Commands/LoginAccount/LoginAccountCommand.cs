using HR_Application.Features.Auth.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Auth.Commands.LoginAccount
{
    public record LoginAccountCommand(LoginDto Dto):IRequest<AuthResponseDto>;
   
}
