using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calceus.Shared
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo url es obligatorio")]
        public string Url { get; set; } = string.Empty;
        public bool Visible { get; set; } = true;
        public bool Deleted { get; set; } = false;
        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
    }
}
