using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGProHackathonAPI.Shared.UtilityModels
{
    public class ProjectTasksForExcel
    {
        //[ExcelColumn(Header="", Format="")]
        public string ProjectName { get; set; }
        public string TaskName { get; set; }
        public string BillableDefault { get; set; }
        public string ProjectExternalSystemKey { get; set; }
        public string TaskExternalSystemKey { get; set; }
        public double? TotalTimeInHours { get; set; }
        public double? TotalBillableTimeInHours { get; set; }
    }
}
