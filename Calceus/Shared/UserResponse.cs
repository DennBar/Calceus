using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calceus.Shared
{
    public class UserResponse
    {
        public List<User> Users { get; set; } = new List<User>();
        public int Pages { get; set; }
        public int PageIndex { get; set; }
    }
}
