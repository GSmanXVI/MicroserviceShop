using MediatR;
using StepShop.CartApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StepShop.CartApi.Requests.Carts.GetUserCart
{
    public class GetUserCartQueryHandler : IRequestHandler<GetUserCartQuery, Cart>
    {
        public Task<Cart> Handle(GetUserCartQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
