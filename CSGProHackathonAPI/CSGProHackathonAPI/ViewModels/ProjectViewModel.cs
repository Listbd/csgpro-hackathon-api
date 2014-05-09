using CSGProHackathonAPI.Shared.Infrastructure;
using CSGProHackathonAPI.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSGProHackathonAPI.ViewModels
{
    public class ProjectViewModel : BaseViewModel<Project>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        [MaxLength(50)]
        public string ExternalSystemKey { get; set; }

        public override Project GetModel(User currentUser)
        {
            return new Project()
            {
                UserId = currentUser.UserId,
                Name = Name,
                ExternalSystemKey = ExternalSystemKey
            };
        }

        public override void UpdateModel(Project model)
        {
            model.Name = Name;
            model.ExternalSystemKey = ExternalSystemKey;
        }
    }
}