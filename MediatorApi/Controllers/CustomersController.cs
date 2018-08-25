using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatorApi.Requests;
using MediatorApi.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediatorApi.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly IMediator _mediator;
        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<GetCustomerResponse>> Get([FromRoute]GetCustomerRequest request) => await _mediator.Send(request);

        [HttpGet]
        public async Task<IEnumerable<GetCustomerResponse>> Get(GetCustomersRequest request) => await _mediator.Send(request);


    }

}
