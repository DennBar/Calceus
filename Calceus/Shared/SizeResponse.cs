using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calceus.Shared
{
    public class SizeResponse
    {
        public List<Size> Sizes { get; set; } = new List<Size>();
        public int Pages { get; set; }
        public int PageIndex { get; set; }
    }
}
