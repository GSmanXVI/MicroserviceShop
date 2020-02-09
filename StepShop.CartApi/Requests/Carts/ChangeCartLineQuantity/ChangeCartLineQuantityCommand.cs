using MediatR;
using StepShop.CartApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StepShop.CartApi.Requests.Carts.ChangeCartLineQuantity
{
    public class ChangeCartLineQuantityCommand : IRequest
    {
        public string UserName { get; set; }
        public CartLine CartLine { get; set; }
    }
}
