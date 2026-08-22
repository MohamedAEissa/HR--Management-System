using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.SalaryReports.DTOs
{
    public class RequestSalaryReportDto
    {
        public Guid EmployeeId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
