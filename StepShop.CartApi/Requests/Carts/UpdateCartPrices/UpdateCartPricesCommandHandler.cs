using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StepShop.CartApi.Requests.Carts.UpdateCartPrices
{
    public class UpdateCartPricesCommandHandler : IRequestHandler<UpdateCartPricesCommand>
    {
        public Task<Unit> Handle(UpdateCartPricesCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
