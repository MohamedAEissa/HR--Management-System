using HR_Application.Features.GeneralSettings.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.GeneralSettings.Commands.UpdateGeneralSettings
{
    public record UpdateGeneralSettingsCommand(UpdateGeneralSettingsDto Dto) : IRequest<bool>;
}
