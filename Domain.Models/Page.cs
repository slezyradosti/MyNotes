using Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Page : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public Guid UnitId { get; set; }
        [JsonIgnore]
        public Unit Unit { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}
