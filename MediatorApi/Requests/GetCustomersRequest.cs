using MediatorApi.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatorApi.Requests
{
    public class GetCustomersRequest : IRequest<IEnumerable<GetCustomerResponse>>
    {
    }
}
