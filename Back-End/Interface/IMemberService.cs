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
         Member GetMemberInformation(string account);
    }
}