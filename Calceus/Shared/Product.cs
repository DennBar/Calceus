using System;
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
        public Category? Category { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una categoría")]
        public int CategoryId { get; set; } = 0;
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        public string Description { get; set; } = string.Empty;
        public List<Image> Images { get; set; } = new List<Image>();
        public bool Visible { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        [NotMapped]
        public bool IsNew { get; set; } = false;
        [NotMapped]
        public bool Editing { get; set; } = false;
    }
}
