using HR_Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Domain.Entities
{
    public class Employee:BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string NationalId { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; 
        public string Phone { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }

        public decimal Salary { get; set; }
        public DateTime ContractDate { get; set; }
        public TimeSpan CheckInTime { get; set; }
        public TimeSpan CheckOutTime { get; set; }

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = null!;

        public Guid? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
