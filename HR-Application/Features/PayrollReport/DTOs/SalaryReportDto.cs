using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.PayrollReport.DTOs
{
    public class SalaryReportDto
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal BasicSalary { get; set; }
        public int AttendanceDays { get; set; }
        public int AbsenceDays { get; set; }
        public decimal TotalOvertimeHours { get; set; }
        public decimal TotalDeductionHours { get; set; }
        public decimal TotalOvertimeAmount { get; set; }
        public decimal TotalDeductionAmount { get; set; }
        public decimal NetSalary { get; set; }
    }
}
