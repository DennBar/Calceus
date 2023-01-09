using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calceus.Shared
{
    public class ColorResponse
    {
        public List<Color> Colors { get; set; } = new List<Color>();
        public int Pages { get; set; }
        public int PageIndex { get; set; }
    }
}
