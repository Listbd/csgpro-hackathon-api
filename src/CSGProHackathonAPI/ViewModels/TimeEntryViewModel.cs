using CSGProHackathonAPI.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSGProHackathonAPI.ViewModels
{
    public class TimeEntryViewModel : BaseViewModel<TimeEntry>
    {
        [Required]
        public int? ProjectRoleId { get; set; }

        [Required]
        public int? ProjectTaskId { get; set; }

        public bool Billable { get; set; }

        [Required]
        public DateTime? TimeIn { get; set; }

        public DateTime? TimeOut { get; set; }

        public decimal? Hours { get; set; }

        public string Comment { get; set; }

        public override TimeEntry GetModel(User currentUser)
        {
            return new TimeEntry()
            {
                UserId = currentUser.UserId,
                ProjectRoleId = ProjectRoleId.Value,
                ProjectTaskId = ProjectTaskId.Value,
                Billable = Billable,
                TimeInUtc = currentUser.ConvertLocalTimeToUtc(TimeIn.Value),
                TimeOutUtc = currentUser.UseStopwatchApproachToTimeEntry && TimeOut != null ? 
                    currentUser.ConvertLocalTimeToUtc(TimeOut.Value) : (DateTime?)null,
                Hours = !currentUser.UseStopwatchApproachToTimeEntry && Hours != null ? 
                    Hours.Value : (decimal?)null,
                Comment = Comment
            };
        }

        public override void UpdateModel(TimeEntry model, User currentUser)
        {
            model.ProjectRoleId = ProjectRoleId.Value;
            model.ProjectTaskId = ProjectTaskId.Value;
            model.Billable = Billable;
            model.TimeInUtc = currentUser.ConvertLocalTimeToUtc(TimeIn.Value);
            model.TimeOutUtc = currentUser.UseStopwatchApproachToTimeEntry && TimeOut != null ? 
                currentUser.ConvertLocalTimeToUtc(TimeOut.Value) : (DateTime?)null;
            model.Hours = !currentUser.UseStopwatchApproachToTimeEntry && Hours != null ? 
                Hours.Value : (decimal?)null;
            model.Comment = Comment;
        }
    }
}