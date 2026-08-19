using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.OfficialHoliday.DTOs
{
    public class CreateOfficialHolidayDto
    {
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
