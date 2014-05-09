using CSGProHackathonAPI.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSGProHackathonAPI.ViewModels
{
    public class ProjectTaskViewModel : BaseViewModel<ProjectTask>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public bool Billable { get; set; }
        
        public bool RequireComment { get; set; }
        
        [MaxLength(50)]
        public string ExternalSystemKey { get; set; }

        public override ProjectTask GetModel(User currentUser)
        {
            return new ProjectTask()
            {
                Name = Name,
                Billable = Billable,
                RequireComment = RequireComment,
                ExternalSystemKey = ExternalSystemKey
            };
        }

        public override void UpdateModel(ProjectTask model, User currentUser)
        {
            model.Name = Name;
            model.Billable = Billable;
            model.RequireComment = RequireComment;
            model.ExternalSystemKey = ExternalSystemKey;
        }
    }
}