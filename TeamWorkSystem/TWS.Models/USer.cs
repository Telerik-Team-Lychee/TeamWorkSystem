namespace TWS.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class User : IdentityUser
    {
        private ICollection<Assignment> assignments;
        private ICollection<TeamWorkRequest> teamWorkRequests;
        private ICollection<TeamWork> teamWorks;

        public User()
        {
            this.assignments = new HashSet<Assignment>();
            this.teamWorkRequests = new HashSet<TeamWorkRequest>();
            this.teamWorks = new HashSet<TeamWork>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        [MaxLength(20)]
        [MinLength(2)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        [MinLength(2)]
        public string LastName { get; set; }

        public bool IsOnline { get; set; }

        public virtual ICollection<Assignment> Assignments
        {
            get
            {
                return this.assignments;
            }

            set
            {
                this.assignments = value;
            }
        }

        public virtual ICollection<TeamWorkRequest> TeamWorkRequests
        {
            get
            {
                return this.teamWorkRequests;
            }

            set
            {
                this.teamWorkRequests = value;
            }
        }

        public virtual ICollection<TeamWork> TeamWork
        {
            get
            {
                return this.teamWorks;
            }

            set
            {
                this.teamWorks = value;
            }
        }
    }
}
