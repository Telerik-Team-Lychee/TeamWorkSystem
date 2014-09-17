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
		private ICollection<UsersTeamWorks> teamWorks;
		private ICollection<Message> messages;
		private ICollection<Resource> resources;

		public User()
		{
			this.assignments = new HashSet<Assignment>();
			this.teamWorkRequests = new HashSet<TeamWorkRequest>();
			this.teamWorks = new HashSet<UsersTeamWorks>();
			this.messages = new HashSet<Message>();
			this.resources = new HashSet<Resource>();
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

		public virtual ICollection<UsersTeamWorks> TeamWorks
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

		public virtual ICollection<Message> Messages
		{
			get
			{
				return this.messages;
			}

			set
			{
				this.messages = value;
			}
		}

		public virtual ICollection<Resource> Resources
		{
			get
			{
				return this.resources;
			}

			set
			{
				this.resources = value;
			}
		}
	}
}