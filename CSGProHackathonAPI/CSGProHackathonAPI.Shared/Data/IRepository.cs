using System;
using System.Collections.Generic;
using CSGProHackathonAPI.Shared.Models;

namespace CSGProHackathonAPI.Shared.Data
{
    public interface IRepository
    {
        void DeleteProject(Project project);
        void DeleteProjectRole(ProjectRole projectRole);
        void DeleteProjectTask(ProjectTask projectTask);
        void DeleteTimeEntry(TimeEntry timeEntry);
        void DeleteUser(User user);
        Project GetProject(int projectId);
        List<Project> GetProjectHours(int userId, DateTime dateUtcStart, DateTime dateUtcEnd);
        ProjectRole GetProjectRole(int projectRoleId);
        List<ProjectRole> GetProjectRoles(int projectId);
        List<Project> GetProjects(int userId);
        ProjectTask GetProjectTask(int projectTaskId);
        List<ProjectTask> GetProjectTasks(int projectId);
        List<TimeEntry> GetTimeEntries(DateTime date, User user);
        TimeEntry GetTimeEntry(int timeEntryId);
        User GetUser(string userName);
        User GetUser(int userId);
        User LoginUser(string userName, string password);
        void SaveProject(Project project);
        void SaveProjectRole(ProjectRole projectRole);
        void SaveProjectTask(ProjectTask projectTask);
        void SaveTimeEntry(TimeEntry timeEntry, User user);
        void SaveUser(User user);
    }
}