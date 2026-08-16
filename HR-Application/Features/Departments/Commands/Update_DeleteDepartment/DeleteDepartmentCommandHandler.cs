using HR_Application.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Departments.Commands.Update_DeleteDepartment
{
    public record DeleteDepartmentCommand(Guid Id) : IRequest<bool>;
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteDepartmentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

            if (department == null)
                throw new KeyNotFoundException($"Department with ID '{request.Id}' was not found.");

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
