using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class PageDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid UnitId { get; set; }
    }
}
