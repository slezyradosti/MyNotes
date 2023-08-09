using Domain.Models.Base;

namespace Domain.Models
{
    public class Note : BaseEntity
    {
        public string Record { get; set; }
        public Guid PageId { get; set; }
        public Page Page { get; set; }
    }
}
