using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StepShop.CartApi.Requests.Carts.ChangeCartLineQuantity
{
    public class ChangeCartLineQuantityCommandHandler : IRequestHandler<ChangeCartLineQuantityCommand>
    {
        public Task<Unit> Handle(ChangeCartLineQuantityCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
