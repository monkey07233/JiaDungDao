using System.Collections.Generic;
using System.Threading.Tasks;
using Back_End.Models;

namespace Back_End.Interface {
    public interface IMemberRepo {
        string Register (Member member);
        Task<Member> GetMemberByLogin (string m_account, string hash_m_password);
        Member GetMemberByAcc (string m_account);
        string EditMemberInformation (Member originMember, UpdateMemberInfo editMember);
        string VerifyAccount (Member member);
        bool UpdatePassword (Member member, string newPassword);
        bool updateMemberImgUrl (int Id);
        bool ApplyResAdmin (Application apply);
        List<Member> GetAllMember ();
        Application GetApplyByAcc (string account);
        bool VerifyApplication (bool pass, string account);
        bool BlockMember (Member blockMember);
         List<Application> GetAllApplication();
    }
}