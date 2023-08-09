﻿using Domain.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Page : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Note> Notes { get; set; }
    }
}
