using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calceus.Shared
{
    public class Store
    {
        public int Id { get; set; }       
        public Product? Product { get; set; }
        public int? ProductId { get; set; }
        public Size? Size { get; set; }
        public int SizeId { get; set; }
        public Color? Color { get; set; }
        public int ColorId { get; set; }
        [Required(ErrorMessage = "El campo precio es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "El valor debe ser mayor a 1")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool Visible { get; set; } = false;
        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
    }
}
