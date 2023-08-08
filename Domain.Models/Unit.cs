using Domain.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Unit : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public Notebook Notebook { get; set; }

        public ICollection<Page> Pages { get; set; } = new List<Page>();
    }
}
