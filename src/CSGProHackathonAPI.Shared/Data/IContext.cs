using System.Data.Entity;
using System.Threading.Tasks;
using CSGProHackathonAPI.Shared.Models;

namespace CSGProHackathonAPI.Shared.Data
{
    public interface IContext
    {
        DbSet<ProjectRole> ProjectRoles { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<ProjectTask> ProjectTasks { get; set; }
        DbSet<TimeEntry> TimeEntries { get; set; }
        DbSet<User> Users { get; set; }

        void DeleteEntity(object entity);
        Task DeleteEntityAsync(object entity);
        void PrepareEntityForSave<TEntityType>(TEntityType entity) where TEntityType : BaseModel;
        void SaveEntity<TEntityType>(TEntityType entity) where TEntityType : BaseModel;
        Task SaveEntityAsync<TEntityType>(TEntityType entity) where TEntityType : BaseModel;
    }
}