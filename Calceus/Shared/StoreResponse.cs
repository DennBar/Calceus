using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calceus.Shared
{
    public class StoreResponse
    {
        public List<Store> Stores { get; set; } = new List<Store>();
        public int Pages { get; set; }
        public int PageIndex { get; set; }
    }
}
