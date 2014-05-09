using CSGProHackathonAPI.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSGProHackathonAPI.ViewModels
{
    public class ProjectRoleViewModel : BaseViewModel<ProjectRole>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string ExternalSystemKey { get; set; }

        public override ProjectRole GetModel(User currentUser)
        {
            return new ProjectRole()
            {
                Name = Name,
                ExternalSystemKey = ExternalSystemKey
            };
        }

        public override void UpdateModel(ProjectRole model)
        {
            model.Name = Name;
            model.ExternalSystemKey = ExternalSystemKey;
        }
    }
}