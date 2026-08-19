using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.GeneralSettings.DTOs
{
    public class UpdateGeneralSettingsDto
    {
        public decimal OvertimeHourRate { get; set; }
        public decimal DeductionHourRate { get; set; }
        public string WeeklyDaysOff { get; set; } = "Friday,Saturday";
    }
}
