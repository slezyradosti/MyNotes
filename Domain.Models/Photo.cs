using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Photo
    {
        //[Key]
        public string Id { get; set; } // cloudinary id
        public string Url { get; set; }
        public string? AdditionalInfo { get; set; } = null;
        public Guid NoteId { get; set; }
        [JsonIgnore]
        public Note Note { get; set; }
    }
}
