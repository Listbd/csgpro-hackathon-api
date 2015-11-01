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
        public string ProjectName
        {
            get
            {
                string value = null;

                var projectRole = ProjectRole;
                if (projectRole != null)
                {
                    var project = projectRole.Project;
                    if (project != null)
                    {
                        value = project.Name;
                    }
                }

                return value;
            }
        }
        public int ProjectRoleId { get; set; }
        public string ProjectRoleName
        {
            get
            {
                string value = null;

                var projectRole = ProjectRole;
                if (projectRole != null)
                {
                    value = projectRole.Name;
                }

                return value;
            }
        }
		public int ProjectTaskId { get; set; }
        public string ProjectTaskName
        {
            get
            {
                string value = null;

                var projectTask = ProjectTask;
                if (projectTask != null)
                {
                    value = projectTask.Name;
                }

                return value;
            }
        }
        public bool Billable { get; set; }
        [JsonIgnore]
        public DateTime TimeInUtc { get; set; }
        public DateTime? TimeIn
        {
            get
            {
                DateTime? value = null;

                var user = User;
                if (user != null)
                {
                    value = user.ConvertUtcToLocalTime(TimeInUtc);
                }

                return value;
            }
        }
        [JsonIgnore]
        public DateTime? TimeOutUtc { get; set; }
        public DateTime? TimeOut
        {
            get
            {
                DateTime? value = null;

                var user = User;
                var timeOutUtc = TimeOutUtc;
                if (user != null && timeOutUtc != null)
                {
                    value = user.ConvertUtcToLocalTime(timeOutUtc.Value);
                }

                return value;
            }
        }
        public decimal? Hours { get; set; }
        public TimeSpan TotalTime
		{
			get
			{
                var value = new TimeSpan();

                if (User.UseStopwatchApproachToTimeEntry)
                {
                    var timeOutUtc = TimeOutUtc ?? DateTime.UtcNow;
                    if (timeOutUtc >= TimeInUtc)
                        value = timeOutUtc - TimeInUtc;
                }
                else
                {
                    var hours = Hours;
                    if (hours != null)
                        value = TimeSpan.FromHours((double)Hours.Value);
                }

                return value;
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
