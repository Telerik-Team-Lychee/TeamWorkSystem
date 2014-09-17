namespace TWS.Models
{
	using System;
	using System.ComponentModel.DataAnnotations.Schema;

	public class TeamWorkRequest
	{
		public int Id { get; set; }

		public string Message { get; set; }

		public string SentById { get; set; }

		public virtual User SentBy { get; set; }

		public int TeamWorkId { get; set; }

		public TeamWork TeamWork { get; set; }
	}
}
