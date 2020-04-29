using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Back_End.Interface;
using Back_End.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_End.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService RestaurantService;
        public static IWebHostEnvironment _environment;
        public RestaurantController(IRestaurantService restaurantService, IWebHostEnvironment environment)
        {
            this.RestaurantService = restaurantService;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult GetAllRestaurant()
        {
            var result = RestaurantService.GetAllRestaurant();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("目前沒有餐廳登錄");
            }

        }

        [HttpGet]
        public IActionResult GetRestaurantInfoById(int Id)
        {
            var result = RestaurantService.GetRestaurantInfoById(Id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("查無此餐廳");
        }

        [HttpPost]
        [Authorize]
        public IActionResult updateRestaurant(Restaurant restaurant)
        {
            var result = RestaurantService.updateRestaurant(restaurant);
            if (result == "success")
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult createRestaurant(Restaurant restaurant)
        {
            var result = RestaurantService.createRestaurant(restaurant);
            if (result == "successed")
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> uploadRestaurantImg([FromForm] RestaurantInfo restaurantInfo)
        {
            try
            {
                string root = _environment.ContentRootPath;
                string rootFile = string.Empty;
                string imgUrl = string.Empty;
                if (restaurantInfo.files != null)
                {
                    //uploadType 0:餐廳封面 ,  1:菜單圖片 
                    switch (restaurantInfo.uploadType)
                    {
                        case 0:
                            root += "\\File\\Restaurant\\";
                            rootFile = root + restaurantInfo.RestaurantID + "_" + restaurantInfo.r_name + "_餐廳封面_" + restaurantInfo.files.FileName;
                            imgUrl = restaurantInfo.RestaurantID + "_" + restaurantInfo.r_name + "_餐廳封面_" + restaurantInfo.files.FileName;
                            RestaurantService.uploadRestaurantImg(restaurantInfo.RestaurantID, imgUrl);
                            break;
                        case 1:
                            root += "\\File\\Menu\\";
                            rootFile = root + restaurantInfo.MenuID + "_" + restaurantInfo.m_item + "_菜單圖片_" + restaurantInfo.files.FileName;
                            imgUrl = restaurantInfo.MenuID + "_" + restaurantInfo.m_item + "_菜單圖片_" + restaurantInfo.files.FileName;
                            RestaurantService.uploadMenuImg(restaurantInfo.MenuID, imgUrl);
                            break;
                    }
                    if (!Directory.Exists(root))
                    {
                        Directory.CreateDirectory(root);
                    }
                    using (FileStream stream = System.IO.File.Create(rootFile))
                    {
                        await restaurantInfo.files.CopyToAsync(stream);
                        stream.Flush();
                    }
                    return Ok("上傳完成");
                }
                return BadRequest("上傳失敗");
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddMenuItem(Menu newMenuItem)
        {
            int res = RestaurantService.AddMenuItem(newMenuItem);
            if (res > 0)
                return Ok();
            else
                return BadRequest("新增menu餐點失敗");
        }

        [HttpGet]
        [Authorize]
        public IActionResult DeleteMenu(int MenuID)
        {
            var IsSuccess = RestaurantService.DeleteMenu(MenuID);
            if (IsSuccess)
            {
                return Ok("刪除菜單成功");
            }
            return BadRequest("刪除菜單失敗");
        }

        [HttpGet]
        [Authorize]
        public IActionResult DeleteRestaurant(int RestaurantID)
        {
            var IsSuccess = RestaurantService.DeleteRestaurant(RestaurantID);
            if (IsSuccess)
            {
                return Ok("刪除餐廳成功");
            }
            return BadRequest("刪除餐廳失敗");
        }

        [HttpPost]
        [Authorize]
        public IActionResult updateMenu(Menu menu)
        {
            var result = RestaurantService.updateMenu(menu);
            if (result == "successed")
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetLatestMenuId()
        {
            var menuId = RestaurantService.GetLatestMenuId();
            return Ok(menuId);
        }
    }
}