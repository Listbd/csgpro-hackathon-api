using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGProHackathonAPI.Shared.Models
{
    public class ProjectTask : BaseModel
	{
		public int ProjectTaskId { get; set; }
        [JsonIgnore]
        public int ProjectId { get; set; }
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }
		public bool Billable { get; set; }
		public bool RequireComment { get; set; }
        [MaxLength(50)]
        public string ExternalSystemKey { get; set; }

        [JsonIgnore]
        public Project Project { get; set; }
	}
}
