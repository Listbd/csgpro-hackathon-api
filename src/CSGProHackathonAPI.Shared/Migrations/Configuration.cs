namespace CSGProHackathonAPI.Shared.Migrations
{
    using CSGProHackathonAPI.Shared.Data;
    using CSGProHackathonAPI.Shared.Infrastructure;
    using CSGProHackathonAPI.Shared.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Context context)
        {
            var user = new User()
            {
                UserId = 1,
                UserName = "johns",
                HashedPassword = Security.GetSwcSH1("password"),
                Name = "John Smith",
                Email = "john@smith.com",
                TimeZoneId = "Pacific Standard Time",
                UseStopwatchApproachToTimeEntry = true
            };
            context.Users.AddOrUpdate(user);

            AddProject(context, 1, 1, "Claims Management System - Elaboration Phase");
            AddProject(context, 2, 1, "Barcode Scanning App");
            AddProject(context, 3, 1, "Events - Phase 1");
            AddProject(context, 4, 1, "Tracker 2 Development");
            AddProject(context, 5, 1, "Tracker 2 Maintenance");
            AddProject(context, 6, 1, "Managed Services - Web Apps");
            AddProject(context, 7, 1, "Internal Projects - App Dev");

            AddProjectRole(context, 1, 1, "Default", "John Smith");
            AddProjectRole(context, 2, 1, "Default", "Project Manager");
            AddProjectRole(context, 3, 1, "Default", "Analyst");
            AddProjectRole(context, 4, 1, "Default", "Architect");
            AddProjectRole(context, 5, 1, "Default", "Architect");
            AddProjectRole(context, 6, 1, "Default", "Senior Dev");
            AddProjectRole(context, 7, 1, "Default", "John Smith");

            AddProjectTask(context, 1, 1, true, true);
            AddProjectTask(context, 2, 2, true, true);
            AddProjectTask(context, 3, 3, true, true);
            AddProjectTask(context, 4, 4, true, true);
            AddProjectTask(context, 5, 5, true, true);
            AddProjectTask(context, 6, 6, true, true);
            AddProjectTask(context, 7, 7, false, true, "Pre Sales");
            AddProjectTask(context, 8, 7, false, true, "Training/Conference");
            AddProjectTask(context, 9, 7, false, true, "Travel Time");
            AddProjectTask(context, 10, 7, false, true, "Research");
            AddProjectTask(context, 11, 7, false, true, "Practice Development");
            AddProjectTask(context, 12, 7, false, true, "Practice Administration");
            AddProjectTask(context, 13, 7, false, true, "Team Meeting");
            AddProjectTask(context, 14, 7, false, true, "1:1 Team Member Meeting");
        }

        private void AddProject(Context context, int projectId, int userId, string name, bool archived = false)
        {
            var project = new Project()
            {
                ProjectId = projectId,
                UserId = userId,
                Name = name,
                Archived = archived
            };
            context.Projects.AddOrUpdate(project);
        }

        private void AddProjectRole(Context context, int projectRoleId, int projectId, string name, 
            string externalSystemKey = null)
        {
            var projectRole = new ProjectRole()
            {
                ProjectRoleId = projectRoleId,
                ProjectId = projectId,
                Name = name,
                ExternalSystemKey = externalSystemKey
            };
            context.ProjectRoles.AddOrUpdate(projectRole);
        }

        private void AddProjectTask(Context context, int projectTaskId, int projectId, bool billable,
            bool requireComment, string name = "Default")
        {
            var projectTask = new ProjectTask()
            {
                ProjectTaskId = projectTaskId,
                ProjectId = projectId,
                Billable = billable,
                RequireComment = requireComment,
                Name = name
            };
            context.ProjectTasks.AddOrUpdate(projectTask);
        }
    }
}
