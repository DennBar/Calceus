using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calceus.Shared
{
    public class OrderVendorResponse
    {
        public List<OrderItem> OrderVendorItems { get; set; } = new List<OrderItem>();
    }
}
