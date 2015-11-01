using CSGProHackathonAPI.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSGProHackathonAPI.ViewModels
{
    public class ProjectRoleStandaloneViewModel : ProjectRoleViewModel
    {
        [Required]
        public int? ProjectId { get; set; }

        public override ProjectRole GetModel(User currentUser)
        {
            var projectRole = base.GetModel(currentUser);
            projectRole.ProjectId = ProjectId.Value;
            return projectRole;
        }
    }
}