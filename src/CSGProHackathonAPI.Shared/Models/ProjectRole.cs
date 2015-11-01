using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGProHackathonAPI.Shared.Models
{
    public class ProjectRole : BaseModel
    {
        public ProjectRole()
        {
            TimeEntries = new List<TimeEntry>();
        }

        public int ProjectRoleId { get; set; }
        [JsonIgnore]
        public int ProjectId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string ExternalSystemKey { get; set; }

        [JsonIgnore]
        public Project Project { get; set; }

        [JsonIgnore]
        public List<TimeEntry> TimeEntries { get; set; }

        public double? TotalTimeInHours
        {
            get
            {
                double? totalTimeInHours = null;

                var timeEntries = TimeEntries;
                if (timeEntries != null && timeEntries.Count > 0)
                {
                    totalTimeInHours = timeEntries.Sum(te => te.TotalTime.TotalHours);
                }

                return totalTimeInHours;
            }
        }

        public string TotalTimeInHoursDisplay
        {
            get
            {
                var totalTimeInHours = TotalTimeInHours;
                return totalTimeInHours != null ? Math.Round(totalTimeInHours.Value, 2).ToString() : null;
            }
        }

        public double? TotalBillableTimeInHours
        {
            get
            {
                double? totalBillableTimeInHours = null;

                var timeEntries = TimeEntries;
                if (timeEntries != null && timeEntries.Count > 0)
                {
                    totalBillableTimeInHours = timeEntries.Sum(te => te.TotalBillableTime.TotalHours);
                }

                return totalBillableTimeInHours;
            }
        }

        public string TotalBillableTimeInHoursDisplay
        {
            get
            {
                var totalBillableTime = TotalBillableTimeInHours;
                return totalBillableTime != null ? Math.Round(totalBillableTime.Value, 2).ToString() : null;
            }
        }
    }
}
