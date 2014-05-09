using CSGProHackathonAPI.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CSGProHackathonAPI.Shared.Infrastructure
{
    public class UserIdentity : GenericIdentity
    {
        public User User { get; private set; }

        public UserIdentity(User user) : base(user.UserName)
        {
            User = user;
        }
    }
}
