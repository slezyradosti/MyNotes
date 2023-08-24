using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class NotebookDto
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
    }
}
