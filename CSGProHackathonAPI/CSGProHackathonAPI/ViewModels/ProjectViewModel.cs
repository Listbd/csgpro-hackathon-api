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
        public ProjectViewModel()
        {
            ProjectRoles = new List<ProjectRoleViewModel>();
            ProjectTasks = new List<ProjectTaskViewModel>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        [MaxLength(50)]
        public string ExternalSystemKey { get; set; }

        public List<ProjectRoleViewModel> ProjectRoles { get; set; }

        public List<ProjectTaskViewModel> ProjectTasks { get; set; }

        public override Project GetModel(User currentUser)
        {
            return new Project()
            {
                UserId = currentUser.UserId,
                Name = Name,
                ExternalSystemKey = ExternalSystemKey,
                ProjectRoles = ProjectRoles.Select(pr => pr.GetModel(currentUser)).ToList(),
                ProjectTasks = ProjectTasks.Select(pt => pt.GetModel(currentUser)).ToList()
            };
        }

        public override void UpdateModel(Project model)
        {
            model.Name = Name;
            model.ExternalSystemKey = ExternalSystemKey;

            // JCTODO update roles and tasks
            // need to make sure that the project model has the roles and tasks collections populated
            // delete any that have an ID and are no longer in the collection
        }
    }
}