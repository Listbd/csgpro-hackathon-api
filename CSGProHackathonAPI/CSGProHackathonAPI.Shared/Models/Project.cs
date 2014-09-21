using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGProHackathonAPI.Shared.Models
{
	public class Project : BaseModel
	{
		public Project()
		{
            ProjectRoles = new List<ProjectRole>();
			ProjectTasks = new List<ProjectTask>();
		}

		public int ProjectId { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }
        [MaxLength(50)]
        public string ExternalSystemKey { get; set; }
        public bool Archived { get; set; }

        [JsonIgnore]
        public User User { get; set; }
        public List<ProjectRole> ProjectRoles { get; set; }
		public List<ProjectTask> ProjectTasks { get; set; }
	}
}
