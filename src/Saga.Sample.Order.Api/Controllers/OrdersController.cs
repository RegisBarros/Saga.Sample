using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Saga.Sample.Models;
using Saga.Sample.Services;

namespace Saga.Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            return Ok("Hello Sagas");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderRequest request) 
        {
            if(request == null)
                return BadRequest();

            await _orderService.CreateOrder(request);

            return Accepted();
        }
    }
}