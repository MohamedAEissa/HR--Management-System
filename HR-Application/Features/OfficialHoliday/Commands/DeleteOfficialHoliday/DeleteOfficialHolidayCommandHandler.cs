using HR_Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.OfficialHoliday.Commands.DeleteOfficialHoliday
{
    public record DeleteOfficialHolidayCommand(Guid Id) : IRequest<bool>;

    public class DeleteOfficialHolidayCommandHandler : IRequestHandler<DeleteOfficialHolidayCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteOfficialHolidayCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteOfficialHolidayCommand request, CancellationToken cancellationToken)
        {
            var holiday = await _context.OfficialHolidays.FindAsync(new object[] { request.Id }, cancellationToken);
            if (holiday == null) return false;

            _context.OfficialHolidays.Remove(holiday);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
