using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calceus.Shared
{
    public class CategoryResponse
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public int Pages { get; set; }
        public int PageIndex { get; set; }
    }
}
