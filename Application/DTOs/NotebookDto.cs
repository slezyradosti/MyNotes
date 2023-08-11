using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class NotebookDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
