using HR_Application.Features.GeneralSettings.DTOs;
using HR_Application.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.GeneralSettings.Queries.GetGeneralSettings
{
    public record GetGeneralSettingsQuery() : IRequest<GeneralSettingsDto>;
    public class GetGeneralSettingsQueryHandler : IRequestHandler<GetGeneralSettingsQuery, GeneralSettingsDto>
    {
        private readonly IApplicationDbContext _context;

        public GetGeneralSettingsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<GeneralSettingsDto> Handle(GetGeneralSettingsQuery request, CancellationToken cancellationToken)
        {
            var settings = await _context.GeneralSettings.FirstOrDefaultAsync(cancellationToken);

            if (settings == null)
            {
                return new GeneralSettingsDto
                {
                    OvertimeHourRate = 0,
                    DeductionHourRate = 0,
                    WeeklyDaysOff = "Friday,Saturday"
                };
            }
             return new GeneralSettingsDto
             {
                    OvertimeHourRate = settings.OvertimeHourRate,
                    DeductionHourRate = settings.DeductionHourRate,
                    WeeklyDaysOff = settings.WeeklyDaysOff
             };
            }

        }
    }

