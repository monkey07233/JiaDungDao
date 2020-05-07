using System.Collections.Generic;
using System.Threading.Tasks;
using Back_End.Interface;
using Back_End.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Back_End.Controllers {
    [Route ("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase {
        private readonly IOrderService _orderService;
        public OrderController (IOrderService orderService) {
            this._orderService = orderService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult GetOrderInfo (MemberInfo memberInfo) {
            var result = _orderService.GetOrderInfo (memberInfo.m_account);
            if (result != null) {
                return Ok (result);
            }
            return BadRequest ("尚未建立訂單");
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetOrderInfoByResId (int restaurantId) {
            var result = _orderService.GetOrderInfoByResId (restaurantId);
            if (result != null) {
                return Ok (result);
            }
            return BadRequest ("尚未收到訂單");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder (OrderInfo orderInfo) {
            var result = await _orderService.CreateOrder (orderInfo);
            if (result) {
                return Ok ("successed");
            }
            return BadRequest ("訂單建立失敗");
        }
    }
}