using MediatorApi.Data;
using MediatorApi.Domain;
using MediatorApi.Mapper;
using MediatorApi.Requests;
using MediatorApi.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatorApi.Handlers
{
    public class GetCustomerHandler : IRequestHandler<GetCustomerRequest, GetCustomerResponse>
    {
        ApplicationContext _context;
        public GetCustomerHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<GetCustomerResponse> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == request.Id);

            var response = customer.MapTo<GetCustomerResponse>();

            return response;
        }
    }
}
