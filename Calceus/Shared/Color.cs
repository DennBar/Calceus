using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calceus.Shared
{
    public class Color
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        [NotMapped]
        public bool IsNew { get; set; } = false;
        [NotMapped]
        public bool Editing { get; set; } = false;
    }
}
