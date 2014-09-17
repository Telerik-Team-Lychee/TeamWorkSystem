namespace TWS.RestApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq.Expressions;

    using TWS.Models;
    using TWS.Models.Enumerations;

    public class TeamworkModel
    {
        public static Expression<Func<TeamWork, TeamworkModel>> FromTeamwork
        {
            get
            {
                return t => new TeamworkModel
                {
                    Id = t.Id,
                    Description = t.Description,
                    GitHubLink = t.GitHubLink,
                    EndDate = t.EndDate,
                    Category = t.Category
                };
            }
        }

        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [MinLength(5)]
        public string Description { get; set; }

        [Index("IX_Unique_GitHub_Link", IsUnique = true)]
        [MaxLength(1000)]
        [Required]
        public string GitHubLink { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public Category Category { get; set; }
    }
}