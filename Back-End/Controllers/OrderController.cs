using Back_End.Interface;
using Back_End.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Back_End.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService OrderService;
        public OrderController(IOrderService orderService)
        {
            this.OrderService = orderService;
        }
        
        [HttpPost]
        [Authorize]
        public IActionResult GetOrderInfo(OrderInfo orderInfo){
            var result = OrderService.GetOrderInfo(orderInfo.title.m_account);
            if (result != null)
            {
                return Ok(result);
            }else
            {
                return BadRequest("尚未建立訂單");
            }
        }
    }
}