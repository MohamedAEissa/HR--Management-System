using HR_Application.Interfaces.Persistence;

using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.GeneralSettings.Commands.UpdateGeneralSettings
{
    public class UpdateGeneralSettingsCommandHandler : IRequestHandler<UpdateGeneralSettingsCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public UpdateGeneralSettingsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(UpdateGeneralSettingsCommand request, CancellationToken cancellationToken)
        {
            var settings = await _context.GeneralSettings.FirstOrDefaultAsync(cancellationToken);

            if (settings == null)
            {
            
                settings = new HR_Domain.Entities.GeneralSettings();
                _context.GeneralSettings.Add(settings);
            }

          
            settings.OvertimeHourRate = request.Dto.OvertimeHourRate;
            settings.DeductionHourRate = request.Dto.DeductionHourRate;
            settings.WeeklyDaysOff = request.Dto.WeeklyDaysOff;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
