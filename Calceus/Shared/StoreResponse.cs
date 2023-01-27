using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calceus.Shared
{
    public class StoreResponse
    {
        public decimal SizeId { get; set; }
        public List<Store> Stores { get; set; } = new List<Store>();
    }
}
