using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StepShop.CartApi.Requests.Carts.DeleteCartLine
{
    public class DeleteCartLineCommand : IRequest
    {
        public string UserName { get; set; }
        public string ProductId { get; set; }
    }
}
