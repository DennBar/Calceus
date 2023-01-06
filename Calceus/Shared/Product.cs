using System;
using System.Collections.Generic;
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
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public List<Size> Sizes { get; set; } = new List<Size>();
        public bool Visible { get; set; } = true;
        public bool Deleted { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
        [NotMapped]
        public bool Editing { get; set; } = false;

    }
}
