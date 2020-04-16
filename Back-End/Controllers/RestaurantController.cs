using System.Collections.Generic;
using Back_End.Interface;
using Back_End.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Back_End.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService RestaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            this.RestaurantService = restaurantService;
        }

        [HttpGet]
        public IActionResult GetAllRestaurant(){
            var result = RestaurantService.GetAllRestaurant();
            if (result != null)
            {
                return Ok(result);
            }else{
                return BadRequest("目前沒有餐廳登錄");
            }
        }
    }
}