using System.Collections.Generic;
using System.Threading.Tasks;
using Back_End.Models;

namespace Back_End.Interface {
    public interface IMemberRepo {
        string Register (Member member);
        Task<Member> GetMemberByLogin (string m_account, string hash_m_password);
        Member GetMemberByAcc (string m_account);
        Member GetMemberByEmail (string email);
        string EditMemberInformation (Member originMember, MemberInfo editMember);
        string VerifyAccount (Member member);
        bool UpdatePassword (Member member, string newPassword);
        bool UpdateMemberImgUrl (int Id);
        bool ApplyResAdmin (Application apply);
        List<Member> GetAllMember ();
        Application GetApplyByAcc (string account);
        bool VerifyApplication (bool pass, Member updateMember);
        bool BlockMember (Member blockMember);
        List<Application> GetAllApplication ();
    }
}