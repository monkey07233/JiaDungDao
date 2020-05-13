using System.Collections.Generic;
using System.Threading.Tasks;
using Back_End.Models;
using Microsoft.Extensions.Configuration;

namespace Back_End.Interface
{
    public interface IMemberService
    {
         string Register(Member member);
         Task<Member> GetMemberByLogin(string m_account,string m_password);

         string HashPassword(string password);
         string GetJwtToken (IConfiguration configuration, string MemberId, string m_account);
         Member GetMemberByAcc(string account);
         string EditMemberInformation (MemberInfo editMember);
         string VerifyAccount(string account,string validateCode);
         bool UpdatePassword(MemberInfo memberInfo);
         bool ResetPassword(MemberInfo memberInfo);
         bool UpdateMemberImgUrl(int Id);
         bool ApplyResAdmin(Application apply);
         List<Member> GetAllMember();
         bool VerifyApplication(bool pass,string account);
         bool BlockMember(string m_account);
         List<Application> GetAllApplication();
         string CreateValidateCode ();
         bool UnblockMember(string m_account);
    }
}