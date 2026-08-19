using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.PayrollReport.DTOs
{
    public class PayrollReportDto
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public decimal BasicSalary { get; set; }

      
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }
        public decimal TotalOvertimeHours { get; set; }
        public decimal TotalDeductionHours { get; set; }

  
        public decimal OvertimeAmount { get; set; }
        public decimal DeductionAmount { get; set; }
        public decimal NetSalary { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }
    }
}
