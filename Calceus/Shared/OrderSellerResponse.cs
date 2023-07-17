using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calceus.Shared
{
    public class OrderSellerResponse
    {
        public List<OrderItem> OrderSellerItems { get; set; } = new List<OrderItem>();
    }
}
