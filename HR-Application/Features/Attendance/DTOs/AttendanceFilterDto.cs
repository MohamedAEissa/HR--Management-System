    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Attendance.DTOs
{
    public class AttendanceFilterDto
    {
        public Guid? EmployeeId { get; set; }
        public string? EmployeeName{ get; set; }

        public Guid? DepartmentId { get; set; }
        public string? DepartmentName { get; set; } 
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
