using Domain.Models.Base;
using IndentityLogic.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Notebook : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public Guid UserId { get; set; }

        [JsonIgnore]
        public ApplicationUser Author { get; set; }

        public ICollection<Unit> Units { get; set; } = new List<Unit>();
    }
}
