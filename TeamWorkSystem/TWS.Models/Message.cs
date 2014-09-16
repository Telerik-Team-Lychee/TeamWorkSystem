namespace TWS.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Message
    {
        public Message()
        {
            this.PostDate = DateTime.Now;
        }

        public int Id { get; set; }

        [MinLength(1)]
        public string Text { get; set; }

        [Required]
        public DateTime PostDate { get; set; }

        public int TeamWorkId { get; set; }

        public virtual TeamWork TeamWork { get; set; }

        [ForeignKey("User")]
        public string SentById { get; set; }

        public User SentBy { get; set; }
    }
}
