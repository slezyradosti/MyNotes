using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class NoteDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Record { get; set; }
        public Guid PageId { get; set; }
    }
}
