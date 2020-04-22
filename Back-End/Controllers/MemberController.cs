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
                var token = MemberService.GetJwtToken (Configuration, result.MemberId.ToString (), result.m_account);
                return Ok (new { account = result.m_account, token = token, role = result.m_role });
            }
            return BadRequest ("帳號或密碼錯誤");
        }

        [HttpPost]
        [Authorize]
        public IActionResult GetMemberInformation (LoginMemberInfo memberInfo) {
            var result = MemberService.GetMemberByAcc (memberInfo.m_account);
            if (result != null) {
                return Ok (result);
            } else {
                return BadRequest ("找不到會員");
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditMemberInformation (UpdateMemberInfo memberAfterEdit) {
            var result = MemberService.EditMemberInformation (memberAfterEdit);
            if (result == "Update completed!" || result == "查無此會員") {
                return Ok (result);
            } else {
                return BadRequest (result);
            }
        }
    }
}