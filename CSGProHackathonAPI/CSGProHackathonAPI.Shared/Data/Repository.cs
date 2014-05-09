﻿using CSGProHackathonAPI.Shared.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGProHackathonAPI.Shared.Data
{
    public class Repository
    {
        private Context _context;

        public Repository() : this (new Context())
        {
        }

        public Repository(Context context)
        {
            _context = context;
        }

        #region Projects

        public List<Project> GetProjects(int userId)
        {
            return _context.Projects
                .Where(p => p.UserId == userId)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public Project GetProject(int projectId)
        {
            return _context.Projects
                .FirstOrDefault(p => p.ProjectId == projectId);
        }

        public void SaveProject(Project project)
        {
            _context.SaveEntity(project);
        }

        public void DeleteProject(int projectId)
        {
            var project = new Project() { ProjectId = projectId };
            _context.DeleteEntity(project);
        }

        #endregion

        #region Project Roles

        public List<ProjectRole> GetProjectRoles(int projectId)
        {
            return _context.ProjectRoles
                .Where(pr => pr.ProjectId == projectId)
                .OrderBy(pr => pr.Name)
                .ToList();
        }

        public ProjectRole GetProjectRole(int projectRoleId)
        {
            return _context.ProjectRoles
                .FirstOrDefault(pr => pr.ProjectRoleId == projectRoleId);
        }

        public void SaveProjectRole(ProjectRole projectRole)
        {
            _context.SaveEntity(projectRole);
        }

        public void DeleteProjectRole(int projectRoleId)
        {
            var projectRole = new ProjectRole() { ProjectRoleId = projectRoleId };
            _context.DeleteEntity(projectRole);
        }
        
        #endregion

        #region Project Tasks

        public List<ProjectTask> GetProjectTasks(int projectId)
        {
            return _context.ProjectTasks
                .Where(pt => pt.ProjectId == projectId)
                .OrderBy(pt => pt.Name)
                .ToList();
        }

        public ProjectTask GetProjectTask(int projectTaskId)
        {
            return _context.ProjectTasks
                .FirstOrDefault(pt => pt.ProjectTaskId == projectTaskId);
        }

        public void SaveProjectTask(ProjectTask projectTask)
        {
            _context.SaveEntity(projectTask);
        }

        public void DeleteProjectTask(int projectTaskId)
        {
            var projectTask = new ProjectTask() { ProjectTaskId = projectTaskId };
            _context.DeleteEntity(projectTask);
        }
        
        #endregion

        #region Time Entry

        public List<TimeEntry> GetTimeEntries(DateTime date, User user)
        {
            var dateUtcStart = user.ConvertLocalTimeToUtc(date);
            var dateUtcEnd = dateUtcStart.AddDays(1);

            return _context.TimeEntries
                .Include(te => te.ProjectRole)
                .Include(te => te.ProjectRole.Project)
                .Include(te => te.ProjectTask)
                .Include(te => te.ProjectTask.Project)
                .Where(te => te.UserId == user.UserId &&
                    te.TimeInUtc >= dateUtcStart && te.TimeInUtc < dateUtcEnd)
                .OrderBy(te => te.TimeInUtc)
                .ToList();
        }

        public TimeEntry GetTimeEntry(int timeEntryId)
        {
            return _context.TimeEntries
                .Include(te => te.ProjectRole)
                .Include(te => te.ProjectTask)
                .Where(te => te.TimeEntryId == timeEntryId)
                .FirstOrDefault();
        }

        public void SaveTimeEntry(TimeEntry timeEntry, User user)
        {
            // if the user is using the stopwatch approach to time entry...
            if (user.UseStopwatchApproachToTimeEntry)
            {
                // if there's a time entry that doesn't have a time out value
                // then update it to the new time entry's time in value
                var lastTimeEntry = (from te in _context.TimeEntries
                                     where te.TimeEntryId != timeEntry.TimeEntryId && te.TimeOutUtc == null &&
                                         te.TimeInUtc < timeEntry.TimeInUtc
                                     orderby te.TimeInUtc descending
                                     select te).FirstOrDefault();
                if (lastTimeEntry != null)
                    lastTimeEntry.TimeOutUtc = timeEntry.TimeInUtc;
            }

            _context.SaveEntity(timeEntry);
        }

        public void DeleteTimeEntry(int timeEntryId)
        {
            var timeEntry = new TimeEntry() { TimeEntryId = timeEntryId };
            _context.DeleteEntity(timeEntry);
        }
        
        #endregion

        #region User

        public User GetUser(int userId)
        {
            return _context.Users
                .Where(u => u.UserId == userId)
                .FirstOrDefault();
        }

        public void SaveUser(User user)
        {
            _context.SaveEntity(user);
        }

        public void DeleteUser(int userId)
        {
            var user = new User() { UserId = userId };
            _context.DeleteEntity(user);
        }

        #endregion
    }
}