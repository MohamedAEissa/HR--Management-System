using HR_Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Domain.Entities
{
    public class SalaryReport : BaseEntity
    {
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

       
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
    }
}
