﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calceus.Shared
{
    public class StoreResponse
    {
        public int SizeId { get; set; }
        public IEnumerable<Store> Stores { get; set; }
    }
}
