using MediatR;
using StepShop.CartApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StepShop.CartApi.Requests.Carts.GetUserCart
{
    public class GetUserCartQuery : IRequest<Cart>
    {
        public string UserName { get; set; }
    }
}
