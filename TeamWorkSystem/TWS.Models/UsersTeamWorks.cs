namespace TWS.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class UsersTeamWorks
	{
		[Key, ForeignKey("User"), Column(Order = 0)]
		public string Id { get; set; }

		[Key, ForeignKey("TeamWork"), Column(Order = 1)]
		public int TeamWorkId { get; set; }

		public bool IsAdmin { get; set; }

		public virtual User User { get; set; }

		public virtual TeamWork TeamWork { get; set; }
	}
}