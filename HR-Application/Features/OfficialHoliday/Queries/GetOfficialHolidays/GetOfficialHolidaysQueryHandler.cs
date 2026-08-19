using HR_Application.Features.OfficialHoliday.DTOs;
using HR_Application.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.OfficialHoliday.Queries.GetOfficialHolidays
{
    public record GetOfficialHolidaysQuery() : IRequest<List<OfficialHolidayDto>>;

    public class GetOfficialHolidaysQueryHandler : IRequestHandler<GetOfficialHolidaysQuery, List<OfficialHolidayDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetOfficialHolidaysQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OfficialHolidayDto>> Handle(GetOfficialHolidaysQuery request, CancellationToken cancellationToken)
        {
            return await _context.OfficialHolidays
                 .Select(h => new OfficialHolidayDto
                 {
                     Id = h.Id,
                     Name = h.Name,
                     Date = h.Date
                 })
                 .OrderBy(h => h.Date)
                 .ToListAsync(cancellationToken);
        }
    }
    }

