using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StepShop.CartApi.Requests.Carts.AddCartLine
{
    public class AddCartLineCommandHandler : IRequestHandler<AddCartLineCommand>
    {
        public Task<Unit> Handle(AddCartLineCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
