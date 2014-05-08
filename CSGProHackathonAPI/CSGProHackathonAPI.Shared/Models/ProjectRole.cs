using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGProHackathonAPI.Shared.Models
{
    public class ProjectRole : BaseModel
    {
        public int ProjectRoleId { get; set; }
        public int ProjectId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string ExternalSystemKey { get; set; }

        public Project Project { get; set; }
    }
}
