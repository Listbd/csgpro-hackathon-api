using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGProHackathonAPI.Shared.Models
{
    public class User : BaseModel
	{
		public int UserId { get; set; }

        [Required]
		[MaxLength(50)]
		public string UserName { get; set; }
        [Required]
        [MaxLength(255)]
        public string HashedPassword { get; set; }
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }
		[Required]
		[MaxLength(255)]
		public string Email { get; set; }
		[Required]
		[MaxLength(100)]
		public string TimeZoneId { get; set; }
        public bool UseStopwatchApproachToTimeEntry { get; set; }
        [MaxLength(50)]
        public string ExternalSystemKey { get; set; }

		public DateTime ConvertUtcToLocalTime(DateTime utcDateTime)
		{
			return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(utcDateTime, "UTC", TimeZoneId);
		}

		public DateTime ConvertLocalTimeToUtc(DateTime localDateTime)
		{
			return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(localDateTime, TimeZoneId, "UTC");
		}
	}
}
