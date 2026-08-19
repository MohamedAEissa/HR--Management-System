using HR_Application.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.PayrollReport.Commands.DeleteSalaryReport
{
    public class DeleteSalaryReportCommandHandler : IRequestHandler<DeleteSalaryReportCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteSalaryReportCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteSalaryReportCommand request, CancellationToken cancellationToken)
        {
            var report = await _context.SalaryReports
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (report == null) return false;

            _context.SalaryReports.Remove(report);
            await _context.SaveChangesAsync(cancellationToken);

            return true; ;
        }
    }
}
