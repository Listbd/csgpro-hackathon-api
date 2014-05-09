using CSGProHackathonAPI.Shared.Infrastructure;
using CSGProHackathonAPI.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSGProHackathonAPI.ViewModels
{
    public class UserViewModel : BaseViewModel<User>
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

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

        public override User GetModel(User currentUser)
        {
            return new User()
            {
                UserName = UserName,
                HashedPassword = Security.GetSwcSH1(Password),
                Name = Name,
                Email = Email,
                TimeZoneId = TimeZoneId,
                UseStopwatchApproachToTimeEntry = UseStopwatchApproachToTimeEntry,
                ExternalSystemKey = ExternalSystemKey
            };
        }

        public override void UpdateModel(User model)
        {
            model.UserName = UserName;
            model.HashedPassword = Security.GetSwcSH1(Password);
            model.Name = Name;
            model.Email = Email;
            model.TimeZoneId = TimeZoneId;
            model.UseStopwatchApproachToTimeEntry = UseStopwatchApproachToTimeEntry;
            model.ExternalSystemKey = ExternalSystemKey;
        }
    }
}