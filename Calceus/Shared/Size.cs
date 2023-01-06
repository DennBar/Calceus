using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calceus.Shared
{
    public class Size
    {
        public int Id { get; set; }
        public Category? Category { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una categoría")]
        public int CategoryId { get; set; } = 0;
        [Required(ErrorMessage = "El campo centímetro es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "El valor debe ser mayor a 1")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cm { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Si no existe escriba 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Ec { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Si no existe escriba 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Usa { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Si no existe escriba 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Eu { get; set; }
        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
    }
}
