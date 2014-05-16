using CSGProHackathonAPI.Shared.Infrastructure;
using CSGProHackathonAPI.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSGProHackathonAPI.ViewModels
{
    public class UserAddViewModel : UserBaseViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        public override User GetModel(User currentUser)
        {
            var user = base.GetModel(currentUser);

            user.HashedPassword = Security.GetSwcSH1(Password);

            return user;
        }

        public override void UpdateModel(User model, User currentUser)
        {
            base.UpdateModel(model, currentUser);

            model.HashedPassword = Security.GetSwcSH1(Password);
        }
    }
}