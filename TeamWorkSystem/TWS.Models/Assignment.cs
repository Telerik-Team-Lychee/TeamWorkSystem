namespace TWS.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TWS.Models.Enumerations;

    public class Assignment
    {
        private ICollection<User> users;

        public Assignment()
        {
            this.users = new HashSet<User>();
            this.Status = AssignmentStatus.Pending;
        }

        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [MinLength(5)]
        public string Description { get; set; }

        [Range(1, 10)]
        public int Priority { get; set; }

        public AssignmentStatus Status { get; set; }

        public int TeamWorkId { get; set; }

        public virtual TeamWork TeamWork { get; set; }

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
