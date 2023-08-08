using Domain.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Notebook : BaseEntity
    {
        [StringLength(50)]
        public string Name { get; set; }
    }
}
