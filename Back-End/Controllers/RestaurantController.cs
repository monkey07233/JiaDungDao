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

namespace Back_End.Controllers {
    [Route ("api/[controller]/[action]")]
    [ApiController]
    public class RestaurantController : ControllerBase {
        private readonly IRestaurantService _restaurantService;
        public RestaurantController (IRestaurantService restaurantService) {
            this._restaurantService = restaurantService;
        }

        [HttpGet]
        public IActionResult GetAllRestaurant () {
            var result = _restaurantService.GetAllRestaurant ();
            if (result != null) {
                return Ok (result);
            }
            return BadRequest ("目前沒有餐廳登錄");
        }

        [HttpGet]
        public IActionResult GetRestaurantInfoById (int id) {
            var result = _restaurantService.GetRestaurantInfoById (id);
            if (result != null) {
                return Ok (result);
            }
            return BadRequest ("查無此餐廳");
        }

        [HttpPost]
        [Authorize]
        public IActionResult UpdateRestaurant (Restaurant restaurant) {
            var result = _restaurantService.UpdateRestaurant (restaurant);
            if (result) {
                return Ok ("success");
            }
            return BadRequest ("更新失敗");
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateRestaurant (Restaurant restaurant) {
            var result = _restaurantService.CreateRestaurant (restaurant);
            if (result == 0) {
                return BadRequest (result);
            }
            return Ok (result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UploadRestaurantImg ([FromForm] UploadInfo restaurantInfo) {
            var uploadResult = await _restaurantService.UploadImg (restaurantInfo);
            if (uploadResult == "上傳成功") {
                return Ok (uploadResult);
            }
            return BadRequest (uploadResult);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddMenuItem (Menu newMenuItem) {
            var result = _restaurantService.AddMenuItem (newMenuItem);
            if (result) {
                return Ok ("新增餐點成功");
            }
            return BadRequest ("新增餐點失敗");
        }

        [HttpGet]
        [Authorize]
        public IActionResult DeleteMenu (int menuID) {
            var isSuccess = _restaurantService.DeleteMenu (menuID);
            var deleteImg = _restaurantService.DeleteMenuImg (menuID);
            if (isSuccess) {
                if (deleteImg) {
                    return Ok ("刪除菜單成功");
                }
                return BadRequest ("刪除菜單照片失敗");
            }
            return BadRequest ("刪除菜單失敗");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteRestaurant (int restaurantID) {
            var deleteRestaurantImgIsSuccess = _restaurantService.DeleteRestaurantAllImg (restaurantID);
            var isSuccess = await _restaurantService.DeleteRestaurant (restaurantID);
            if (isSuccess && deleteRestaurantImgIsSuccess) {
                return Ok ("刪除餐廳成功");
            }
            return BadRequest ("刪除餐廳失敗");
        }

        [HttpPost]
        [Authorize]
        public IActionResult UpdateMenu (Menu menu) {
            var result = _restaurantService.UpdateMenu (menu);
            if (result) {
                return Ok ("successed");
            }
            return BadRequest ("fail");
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetLatestMenuId () {
            var menuId = _restaurantService.GetLatestMenuId ();
            return Ok (menuId);
        }
    }
}