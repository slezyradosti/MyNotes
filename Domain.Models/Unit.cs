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

        /// <summary>
        /// T[JsonIgnore] using to not include Notebook filed to returning data
        /// it also make dead cycle
        /// TO-DO: Mb there is another way to solve this thing. Try to use DTO!!
        /// </summary>
        [JsonIgnore]
        public Notebook Notebook { get; set; }

        public ICollection<Page> Pages { get; set; } = new List<Page>();
    }
}
