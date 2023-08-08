﻿using Domain.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Notebook : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<Unit> Units { get; set; } = new List<Unit>();
    }
}
