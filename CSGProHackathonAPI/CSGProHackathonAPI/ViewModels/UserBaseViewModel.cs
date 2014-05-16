using CSGProHackathonAPI.Shared.Data;
using CSGProHackathonAPI.Shared.Infrastructure;
using CSGProHackathonAPI.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSGProHackathonAPI.ViewModels
{
    public abstract class UserBaseViewModel : BaseViewModel<User>
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

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

        protected override IEnumerable<ValidationMessage> Validate(Repository repository, User currentUser)
        {
            var userName = UserName;
            if (!string.IsNullOrWhiteSpace(userName))
            {
                var user = repository.GetUser(userName);
                if (user != null && user.UserId != currentUser.UserId)
                {
                    yield return new ValidationMessage(
                        "UserName", 
                        string.Format("The user name '{0}' is already in use by another user.", userName));
                }
            }
        }

        public override User GetModel(User currentUser)
        {
            return new User()
            {
                UserName = UserName,
                Name = Name,
                Email = Email,
                TimeZoneId = TimeZoneId,
                UseStopwatchApproachToTimeEntry = UseStopwatchApproachToTimeEntry,
                ExternalSystemKey = ExternalSystemKey
            };
        }

        public override void UpdateModel(User model, User currentUser)
        {
            model.UserName = UserName;
            model.Name = Name;
            model.Email = Email;
            model.TimeZoneId = TimeZoneId;
            model.UseStopwatchApproachToTimeEntry = UseStopwatchApproachToTimeEntry;
            model.ExternalSystemKey = ExternalSystemKey;
        }
    }
}