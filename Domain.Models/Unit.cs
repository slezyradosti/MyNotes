using Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Unit : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public Guid NotebookId { get; set; }
        [JsonIgnore]
        public Notebook Notebook { get; set; }

        public ICollection<Page> Pages { get; set; } = new List<Page>();
    }
}
