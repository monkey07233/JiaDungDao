using System.Threading.Tasks;
using Back_End.Models;

namespace Back_End.Interface
{
    public interface IMemberRepo
    {
         string Register(Member member);
         Task<Member> GetMemberByLogin(string m_account,string hash_m_password);
         Member isAccountExist(string m_account);
         Member GetMemberInformation(int MemberId);
    }
}