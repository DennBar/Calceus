using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Calceus.Shared
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Order? Order { get; set; }
        public int? OrderId { get; set; }        
        public Product? Product { get; set; }
        public int? ProductId { get; set; }
        public Size? Size { get; set; }
        public int? SizeId { get; set; }
        public Color? Color { get; set; }
        public int? ColorId { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
    }
}
