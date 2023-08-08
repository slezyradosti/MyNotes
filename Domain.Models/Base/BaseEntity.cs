using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Base
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
