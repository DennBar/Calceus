﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calceus.Shared
{
    public class Product
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        public string Description { get; set; } = string.Empty;
        public Color? Color { get; set; }
        public int ColorId { get; set; }
        public bool Visible { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        [NotMapped]
        public bool IsNew { get; set; } = false;
        [NotMapped]
        public bool Editing { get; set; } = false;
    }
}
