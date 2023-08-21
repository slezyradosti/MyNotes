using Domain.Models.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Note : BaseEntity
    {
        [Required]
        [StringLength(50)]
        [DefaultValue("new note")]
        public string Name { get; set; } = "new note";
        public string Record { get; set; }
        public Guid PageId { get; set; }
        [JsonIgnore]
        public Page Page { get; set; }
    }
}
