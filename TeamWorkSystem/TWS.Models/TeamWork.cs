namespace TWS.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class TeamWork
	{
		private ICollection<User> users;
		private ICollection<Message> messages;
		private ICollection<Resource> resources;
		private ICollection<Request> requests;

		public TeamWork()
		{
			this.users = new HashSet<User>();
			this.messages = new HashSet<Message>();
			this.resources = new HashSet<Resource>();
			this.requests = new HashSet<Request>();
			this.StartDate = DateTime.Now;
		}

		public int Id { get; set; }

		[Required]
		[MinLength(2)]
		[MaxLength(50)]
		public string Name { get; set; }

		[MinLength(5)]
		public string Description { get; set; }

		[Required]
		[ForeignKey("User")]
		public string AdminId { get; set; }

		public virtual User Admin { get; set; }

		[Index(IsUnique = true)]
		public string GitHubLink { get; set; }

		[Required]
		public DateTime StartDate { get; set; }

		[Required]
		public DateTime EndDate { get; set; }

		[Required]
		public Category Category { get; set; }

		//public virtual ICollection<Task> Task
		//{
 
		//}

		public virtual ICollection<Request> Requests
		{
			get
			{
				return this.requests;
			}
			set
			{
				this.requests = value;
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

		public virtual ICollection<User> Users
		{
			get
			{
				return this.users;
			}
			set
			{
				this.users = value;
			}
		}
	}
}