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
        [HttpGet]
        public IActionResult GetRestaurantInfoById(int Id){
            var result = RestaurantService.GetRestaurantInfoById(Id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("查無此餐廳");
        }
        [HttpPost]
        [Authorize]
        public IActionResult updateRestaurant(Restaurant restaurant){
            var result = RestaurantService.updateRestaurant(restaurant);
            if (result == "success")
            {
                return Ok(result);
            }else
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult createRestaurant(Restaurant restaurant){
            var result = RestaurantService.createRestaurant(restaurant);
            if (result == "successed")
            {
                return Ok(result);
            }else
            {
                return BadRequest(result);
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddMenuItem(Menu newMenuItem){
            int res=RestaurantService.AddMenuItem(newMenuItem);
            if(res>0)
                return Ok();
            else 
                return BadRequest("新增menu餐點失敗");
        }

        [HttpGet]
        [Authorize]
        public IActionResult DeleteMenu(int MenuID){
            var IsSuccess = RestaurantService.DeleteMenu(MenuID);
            if (IsSuccess)
            {
                return Ok("刪除餐廳成功");
            }
            return BadRequest("刪除餐廳失敗");
        }

        [HttpGet]
        [Authorize]
        public IActionResult DeleteRestaurant(int RestaurantID){
            var IsSuccess = RestaurantService.DeleteRestaurant(RestaurantID);
            if (IsSuccess)
            {
                return Ok("刪除餐廳成功");
            }
            return BadRequest("刪除餐廳失敗");
        }

        [HttpPost]
        [Authorize]
        public IActionResult updateMenu(Menu menu){
            var result = RestaurantService.updateMenu(menu);
            if (result == "successed")
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}