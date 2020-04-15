using Back_End.Models;
namespace Back_End.Interface
{
    public interface IMemberService
    {
         string Register(Member member);
         Member GetMemberByLogin(Member member);

         string HashPassword(string password);
    }
}