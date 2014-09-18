namespace TWS.RestApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using TWS.Models;

    public class AssignmentModel
    {
        public static Expression<Func<Assignment, AssignmentModel>> FromAssignment
        {
            get
            {
                return a => new AssignmentModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    Priority = a.Priority,
                    Status = a.Status.ToString()
                };
            }
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

        public string Status { get; set; }
    }
}