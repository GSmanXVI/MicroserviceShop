using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StepShop.CartApi.Requests.Carts.DeleteCartLine
{
    public class DeleteCartLineCommandHandler : IRequestHandler<DeleteCartLineCommand>
    {
        public Task<Unit> Handle(DeleteCartLineCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
