﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entityes
{
    public class Baskets
    {
        public string Id { get; set; }

        public List<BasketItems> Items { get; set; }
    }
}
