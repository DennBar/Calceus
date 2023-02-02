using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calceus.Shared
{
    public class CartResponse
    {
        public int ProductId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int SizeId { get; set; }
        public decimal Size { get; set; }
        public int ColorId { get; set; }
        public string Color { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
