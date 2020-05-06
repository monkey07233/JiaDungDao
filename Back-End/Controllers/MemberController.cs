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
        private readonly IMailService MailService;
        private readonly IRestaurantService RestaurantService;

        public MemberController (IMemberService memberService, IConfiguration configuration, IMailService mailService, IRestaurantService restaurantService) {
            this.MemberService = memberService;
            this.Configuration = configuration;
            this.MailService = mailService;
            this.RestaurantService = restaurantService;
        }

        [HttpPost]
        public IActionResult Register (Member member) {
            var result = MemberService.Register (member);
            if (result == "successed") {
                MailService.SendMail (member.m_email, member.m_account);
                return Ok (result);
            } else {
                return BadRequest (result);
            }
        }

        [HttpPost]
        public IActionResult VerifyAccount (LoginMemberInfo memberInfo) {
            var result = MemberService.VerifyAccount (memberInfo.m_account);
            if (result == "Verified") {
                return Ok (result);
            } else {
                return BadRequest (result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login (LoginMemberInfo memberInfo) {
            var result = await MemberService.GetMemberByLogin (memberInfo.m_account, memberInfo.m_password);
            if (result != null) {
                if (result.isValid == true) {
                    var token = MemberService.GetJwtToken (Configuration, result.MemberId.ToString (), result.m_account);
                    return Ok (new { account = result.m_account, token = token, role = result.m_role });
                }
                return BadRequest ("帳號未通過驗證");
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

        [HttpPost]
        [Authorize]
        public IActionResult UpdatePassword (UpdateMemberInfo memberInfo) {
            var result = MemberService.UpdatePassword (memberInfo);
            if (result == true) {
                return Ok ("更換密碼成功");
            } else {
                return BadRequest ("更換密碼失敗");
            }
        }

        [HttpPost]
        public IActionResult ResetPassword (UpdateMemberInfo memberInfo) {
            var result = MemberService.ResetPassword (memberInfo);
            if (result == true) {
                return Ok ("重設密碼成功");
            } else {
                return BadRequest ("重設密碼失敗");
            }
        }

        [HttpPost]
        public IActionResult SendResetPasswordMail (UpdateMemberInfo resetInfo) {
            var member = MemberService.GetMemberByAcc (resetInfo.m_account);
            if (member != null) {
                bool result = MailService.SendResetPasswordMail (resetInfo.m_email, member.m_account);
                if (result == true) {
                    return Ok ("寄送重設密碼連結信成功");
                }
                return BadRequest ("寄送重設密碼連結信失敗");
            } else {
                return BadRequest ("帳號不存在");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadUserImg ([FromForm] UploadInfo uploadInfo) {
            var uploadResult = await RestaurantService.UploadImg (uploadInfo);
            var updateMemberImgUrl = MemberService.updateMemberImgUrl (uploadInfo.id);
            if (uploadResult == "上傳成功") {
                if (updateMemberImgUrl) {
                    return Ok (uploadResult);
                }
                return BadRequest ("路徑未更新");
            }
            return BadRequest (uploadResult);
        }

        [HttpPost]
        [Authorize]
        public IActionResult ApplyResAdmin (Application apply) {
            var result = MemberService.ApplyResAdmin (apply);
            if (result)
                return Ok ("申請成功，待審核");
            else
                return BadRequest ("申請失敗");
        }
        [HttpGet]
        [Authorize]
        public IActionResult GetAllMember(){
            var result = MemberService.GetAllMember();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("找不到會員");
        }

        [HttpPost]
        public IActionResult VerifyApplication(bool pass,int appicationId)
        {
            bool result=MemberService.VerifyApplication(pass,appicationId);
            if(result)
                return Ok("審核餐廳管理者申請成功");
            else
                return BadRequest("審核餐廳管理者申請失敗");
        }
    }
}