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
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "El campo centímetro es obligatorio")]
        public int Cm { get; set; }
        public int Ec { get; set; }
        public int Usa { get; set; }
        public int Eu { get; set; }
        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
    }
}
