﻿using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class UnitDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public Guid NotebookId { get; set; }
    }
}
