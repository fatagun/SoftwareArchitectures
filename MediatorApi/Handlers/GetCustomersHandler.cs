using AutoMapper.QueryableExtensions;
using MediatorApi.Data;
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
    public class GetCustomersHandler : IRequestHandler<GetCustomersRequest, IEnumerable<GetCustomerResponse>>
    {
        readonly ApplicationContext _context;
        public GetCustomersHandler(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<GetCustomerResponse>> Handle(GetCustomersRequest request, CancellationToken cancellationToken)
        {
            var customers = await _context.Customers.ToListAsync();

            var response = customers.MapToList<GetCustomerResponse>();

            return response;
        }
    }
}
