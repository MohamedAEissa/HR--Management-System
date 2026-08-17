using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

   
        public Guid RoleId { get; set; }

      
        public Guid? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
