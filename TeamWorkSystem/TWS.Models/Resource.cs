namespace TWS.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Resource
	{
		public int Id { get; set; }

		public virtual User UploadedBy  { get; set; }

		[Required]
		[MinLength(1)]
		[MaxLength(100)]
		public string Name { get; set; }

		[Required]
		public byte[] Data { get; set; }
	}
}