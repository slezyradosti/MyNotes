using Domain.Models.Base;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Note : BaseEntity
    {
        public string Record { get; set; }
        public Guid PageId { get; set; }
        [JsonIgnore]
        public Page Page { get; set; }
    }
}
