using MediatR;
using StepShop.CartApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StepShop.CartApi.Requests.Carts.AddCartLine
{
    public class AddCartLineCommand : IRequest
    {
        public string UserName { get; set; }
        public string ProductId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}
