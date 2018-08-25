using MediatorApi.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatorApi.Requests
{
    public class GetCustomerRequest : IRequest<GetCustomerResponse>
    {
        public int Id { set; get; }
    } 
}
