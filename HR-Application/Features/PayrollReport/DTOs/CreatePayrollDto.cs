using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.PayrollReport.DTOs
{
    public class CreatePayrollDto
    {
        public Guid EmployeeId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal Bonus { get; set; } 
        public decimal Penalties { get; set; } 
        public string? Notes { get; set; }
    }
}
