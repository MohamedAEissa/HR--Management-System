using HR_Domain.Common;
using HR_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Domain.Entities
{
    public class Attendance : BaseEntity
    {
        public DateTime Date { get; set; }
        public TimeSpan? CheckInTime { get; set; }
        public TimeSpan? CheckOutTime { get; set; }

        public decimal OvertimeHours { get; set; } = 0;
        public decimal DeductionHours { get; set; } = 0;

        public AttendanceStatus Status { get; set; } = AttendanceStatus.Present;

        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
    }
}
