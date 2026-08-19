using HR_Application.Features.OfficialHoliday.DTOs;
using HR_Application.Interfaces.Persistence;
using HR_Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.OfficialHoliday.Commands.CreateOfficialHoliday
{
    public class CreateOfficialHolidayCommandHandler : IRequestHandler<CreateOfficialHolidayCommand, OfficialHolidayDto>
    {
        private readonly IApplicationDbContext _context;

        public CreateOfficialHolidayCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OfficialHolidayDto> Handle(CreateOfficialHolidayCommand request, CancellationToken cancellationToken)
        {
            var holiday = new HR_Domain.Entities.OfficialHoliday
            {
                Name = request.Dto.Name,
                Date = request.Dto.Date.Date
            };
            _context.OfficialHolidays.Add(holiday);
            await _context.SaveChangesAsync(cancellationToken);

            return new OfficialHolidayDto
            {
                Id = holiday.Id,
                Name = holiday.Name,
                Date = holiday.Date,
            };
        }
    }
}
