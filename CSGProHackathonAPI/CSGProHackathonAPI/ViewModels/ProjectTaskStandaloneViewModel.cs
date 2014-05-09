using CSGProHackathonAPI.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSGProHackathonAPI.ViewModels
{
    public class ProjectTaskStandaloneViewModel : ProjectTaskViewModel
    {
        [Required]
        public int? ProjectId { get; set; }

        public override ProjectTask GetModel(User currentUser)
        {
            var projectTask = base.GetModel(currentUser);
            projectTask.ProjectId = ProjectId.Value;
            return projectTask;
        }
    }
}