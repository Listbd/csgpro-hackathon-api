﻿using CSGProHackathonAPI.Shared.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGProHackathonAPI.Shared.Data
{
    public class Context : DbContext, IContext
    {
        public Context()
            : base(nameOrConnectionString: "Context")
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectRole> ProjectRoles { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<TimeEntry> TimeEntries { get; set; }
        public DbSet<User> Users { get; set; }

        public void PrepareEntityForSave<TEntityType>(TEntityType entity)
            where TEntityType : BaseModel
        {
            if (entity == null)
            {
                throw new ArgumentException("Cannot prepare a null entity for save.");
            }

            if (entity.IsNew())
                Set<TEntityType>().Add(entity);
            else
                Entry<TEntityType>(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void SaveEntity<TEntityType>(TEntityType entity)
            where TEntityType : BaseModel
        {
            PrepareEntityForSave<TEntityType>(entity);
            SaveChanges();
        }

        public Task SaveEntityAsync<TEntityType>(TEntityType entity)
            where TEntityType : BaseModel
        {
            PrepareEntityForSave<TEntityType>(entity);
            return SaveChangesAsync();
        }

        public void DeleteEntity(object entity)
        {
            Entry(entity).State = EntityState.Deleted;

            SaveChanges();
        }

        public Task DeleteEntityAsync(object entity)
        {
            Entry(entity).State = EntityState.Deleted;

            return SaveChangesAsync();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            var timeEntryEntity = modelBuilder.Entity<TimeEntry>();
            timeEntryEntity.Property(te => te.Hours).HasPrecision(4, 2);
        }
    }
}
