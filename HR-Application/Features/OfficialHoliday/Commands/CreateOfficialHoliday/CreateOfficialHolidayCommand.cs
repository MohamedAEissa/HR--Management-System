using HR_Application.Features.OfficialHoliday.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.OfficialHoliday.Commands.CreateOfficialHoliday
{
    public record CreateOfficialHolidayCommand(CreateOfficialHolidayDto Dto) : IRequest<OfficialHolidayDto>;
}
