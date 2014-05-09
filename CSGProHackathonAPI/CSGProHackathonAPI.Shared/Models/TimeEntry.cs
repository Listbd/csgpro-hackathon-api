using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGProHackathonAPI.Shared.Models
{
    public class TimeEntry : BaseModel
	{
		public int TimeEntryId { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
        public int ProjectRoleId { get; set; }
		public int ProjectTaskId { get; set; }
		public bool Billable { get; set; }
		public DateTime TimeInUtc { get; set; }
		public DateTime? TimeOutUtc { get; set; }
        public decimal? Hours { get; set; }
		public TimeSpan TotalTime
		{
			get
			{
				var timeOutUtc = TimeOutUtc ?? DateTime.UtcNow;
				if (timeOutUtc >= TimeInUtc)
					return timeOutUtc - TimeInUtc;
				else
					return new TimeSpan();
			}
		}
		public string TotalTimeDisplay
		{
			get
			{
				return Math.Round(TotalTime.TotalHours, 2).ToString();
			}
		}
		public TimeSpan TotalBillableTime
		{
			get
			{
				return Billable ? TotalTime : new TimeSpan();
			}
		}
		public string TotalBillableTimeDisplay
		{
			get
			{
				return Math.Round(TotalBillableTime.TotalHours, 2).ToString();
			}
		}
		public string Comment { get; set; }

        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public ProjectRole ProjectRole { get; set; }
        [JsonIgnore]
        public ProjectTask ProjectTask { get; set; }
	}
}
