using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StepShop.CartApi.Models
{
    public class Cart
    {
        public string UserName { get; set; }
        public ICollection<CartLine> CartLines { get; set; }
    }
}
