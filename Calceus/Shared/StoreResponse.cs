using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calceus.Shared
{
    public class StoreResponse
    {
        public decimal SizeEc { get; set; }
        public List<Store> StoreList { get; set; } = new List<Store>();
    }
}
