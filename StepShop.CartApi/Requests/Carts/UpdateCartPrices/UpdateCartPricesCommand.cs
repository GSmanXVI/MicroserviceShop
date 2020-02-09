using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StepShop.CartApi.Requests.Carts.UpdateCartPrices
{
    public class UpdateCartPricesCommand : IRequest
    {
        public string ProductId { get; set; }
        public decimal Price { get; set; }
    }
}
