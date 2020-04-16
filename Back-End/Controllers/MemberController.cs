using System.Threading.Tasks;
using Back_End.Interface;
using Back_End.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Back_End.Controllers {
    [Route ("api/[controller]/[action]")]
    [ApiController]
    public class MemberController : ControllerBase {
        private readonly IMemberService MemberService;
        private readonly IConfiguration Configuration;
        public MemberController (IMemberService memberService, IConfiguration configuration) {
            this.MemberService = memberService;
            this.Configuration = configuration;
        }

        [HttpPost]
        public IActionResult Register (Member member) {
            var result = MemberService.Register (member);
            if (result == "successed" || result == "帳號已存在") {
                return Ok (result);
            } else {
                return BadRequest (result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login (LoginMemberInfo memberInfo) {
            var result = await MemberService.GetMemberByLogin (memberInfo.m_account, memberInfo.m_password);
            if (result != null) {
                result.m_account = "秘密";
                result.m_password = "才不告訴逆嘞";
                var token = MemberService.GetJwtToken (Configuration, result.MemberId.ToString (), result.m_account);
                return Ok (new { userData = result, token = token });
            }
            return BadRequest ("找不到會員");
        }

        [HttpPost]
        [Authorize]
        public IActionResult GetMemberInformation (string account) {
            var result = MemberService.GetMemberInformation (account);
            if (result != null) {
                return Ok (result);
            } else {
                return BadRequest ("找不到會員");
            }
        }
    }
}