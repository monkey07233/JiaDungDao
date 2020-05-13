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
        private readonly IMemberService _memberService;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        private readonly IRestaurantService _restaurantService;

        public MemberController (IMemberService memberService, IConfiguration configuration, IMailService mailService, IRestaurantService restaurantService) {
            this._memberService = memberService;
            this._configuration = configuration;
            this._mailService = mailService;
            this._restaurantService = restaurantService;
        }

        [HttpPost]
        public IActionResult Register (Member member) {
            member.validateCode = _memberService.CreateValidateCode ();
            var result = _memberService.Register (member);
            if (result == "successed") {

                _mailService.SendMail (member.m_email, member.m_account, "呷午餐會員認證", _mailService.GetMailBody (member.m_account, "validate", member.validateCode));
                return Ok (result);
            } else {
                return BadRequest (result);
            }
        }

        [HttpPost]
        public IActionResult VerifyAccount (MemberInfo memberInfo) {
            var result = _memberService.VerifyAccount (memberInfo.m_account, memberInfo.validateCode);
            if (result == "Verified") {
                return Ok (result);
            } else {
                return BadRequest (result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login (MemberInfo memberInfo) {
            var result = await _memberService.GetMemberByLogin (memberInfo.m_account, memberInfo.m_password);
            if (result != null) {
                if (string.IsNullOrEmpty (result.validateCode)) {
                    if (result.isBlock != true) {
                        var token = _memberService.GetJwtToken (_configuration, result.MemberId.ToString (), result.m_account);
                        return Ok (new { account = result.m_account, token = token, role = result.m_role });
                    }
                    return BadRequest ("帳號已被封鎖");
                }
                return BadRequest ("帳號未通過驗證");
            }
            return BadRequest ("帳號或密碼錯誤");
        }

        [HttpPost]
        [Authorize]
        public IActionResult GetMemberInformation (MemberInfo memberInfo) {
            var result = _memberService.GetMemberByAcc (memberInfo.m_account);
            if (result != null) {
                return Ok (result);
            } else {
                return BadRequest ("找不到會員");
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditMemberInformation (MemberInfo memberAfterEdit) {
            var result = _memberService.EditMemberInformation (memberAfterEdit);
            if (result == "Update completed!") {
                return Ok (result);
            } else {
                return BadRequest (result);
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult UpdatePassword (MemberInfo memberInfo) {
            var result = _memberService.UpdatePassword (memberInfo);
            if (result == true) {
                return Ok ("更換密碼成功");
            }
            return BadRequest ("更換密碼失敗");
        }

        [HttpPost]
        public IActionResult ResetPassword (MemberInfo memberInfo) {
            var result = _memberService.ResetPassword (memberInfo);
            if (result == true) {
                return Ok ("重設密碼成功");
            } else {
                return BadRequest ("重設密碼失敗");
            }
        }

        [HttpPost]
        public IActionResult SendResetPasswordMail (MemberInfo resetInfo) {
            var member = _memberService.GetMemberByAcc (resetInfo.m_account);
            if (member != null) {
                bool result = _mailService.SendMail (resetInfo.m_email, member.m_account, "【呷午餐】重設密碼", _mailService.GetMailBody (member.m_account, "reset"));
                if (result == true) {
                    return Ok ("寄送重設密碼連結信成功");
                }
                return BadRequest ("寄送重設密碼連結信失敗");
            } else {
                return BadRequest ("帳號不存在");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UploadUserImg ([FromForm] UploadInfo uploadInfo) {
            var uploadResult = await _restaurantService.UploadImg (uploadInfo);
            var updateMemberImgUrl = _memberService.UpdateMemberImgUrl (uploadInfo.id);
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
            var result = _memberService.ApplyResAdmin (apply);
            if (result)
                return Ok ("申請成功，待審核");
            else
                return BadRequest ("申請失敗");
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllMember () {
            var result = _memberService.GetAllMember ();
            if (result != null) {
                return Ok (result);
            }
            return BadRequest ("找不到會員");
        }

        [HttpPost]
        [Authorize]
        public IActionResult VerifyApplication (bool pass, string account) {
            bool result = _memberService.VerifyApplication (pass, account);
            if (result)
                return Ok ("審核成功");
            else
                return BadRequest ("審核失敗");
        }

        [HttpGet]
        [Authorize]
        public IActionResult BlockMember (string m_account) {
            var result = _memberService.BlockMember (m_account);
            if (result) {
                return Ok ("封鎖成功");
            }
            return BadRequest ("封鎖失敗");
        }

        [HttpGet]
        [Authorize]
        public IActionResult UnblockMember (string m_account) {
            var result = _memberService.UnblockMember (m_account);
            if (result) {
                return Ok ("解除封鎖成功");
            }
            return BadRequest ("解除封鎖失敗");
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllApplication () {
            var result = _memberService.GetAllApplication ();
            if (result != null) {
                return Ok (result);
            }
            return BadRequest ("尚未有申請者");
        }
    }
}