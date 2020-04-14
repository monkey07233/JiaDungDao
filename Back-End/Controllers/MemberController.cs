using Back_End.Interface;
using Back_End.Models;
using Microsoft.AspNetCore.Mvc;

namespace Back_End.Controllers {
    [Route ("api/[controller]/[action]")]
    [ApiController]
    public class MemberController : ControllerBase {
        private readonly IMemberService MemberService;
        public MemberController (IMemberService memberService) {
            this.MemberService = memberService;
        }

        [HttpPost]
        public IActionResult Register (Member member) {
            var result = MemberService.Register (member);
            if (result == "successed") {
                return Ok (result);
            } else {
                return BadRequest ("error");
            }
        }

        []
    }
}