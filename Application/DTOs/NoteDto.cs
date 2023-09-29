using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class NoteDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Record { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        public Guid PageId { get; set; }
    }
}
