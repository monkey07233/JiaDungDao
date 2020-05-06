using System;
using System.Linq;
using System.Threading.Tasks;
using Back_End.Interface;
using Back_End.Models;
using JiaDungDao.Connection;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Repositories {
    public class MemberRepo : IMemberRepo {
        private readonly ApplicationDbContext db;
        public MemberRepo (ApplicationDbContext dbContext) {
            this.db = dbContext;
        }

        public async Task<Member> GetMemberByLogin (string m_account, string hash_m_password) {
            return await db.Member.Where (m => m.m_account == m_account && m.m_password == hash_m_password).FirstOrDefaultAsync ();
        }

        public Member GetMemberByAcc (string m_account) {
            return db.Member.Where (m => m.m_account == m_account).FirstOrDefault ();
        }

        public string Register (Member member) {
            string result = string.Empty;
            try {
                db.Member.Add (member);
                db.SaveChanges ();
                result = "successed";
            } catch (Exception e) {
                result = e.Message.ToString ();
            }
            return result;
        }

        public string VerifyAccount (Member member) {
            try {
                member.isValid = true;
                db.SaveChanges ();
                return "Verified";
            } catch (Exception e) {
                return e.Message.ToString ();
            }
        }

        public string EditMemberInformation (Member originMember, UpdateMemberInfo memberAfterEdit) {
            string result = string.Empty;
            try {
                originMember.m_name = memberAfterEdit.m_name;
                originMember.m_address = memberAfterEdit.m_address;
                originMember.m_birthday = memberAfterEdit.m_birthday;
                originMember.m_email = memberAfterEdit.m_email;
                db.SaveChanges ();
                result = "Update completed!";
            } catch (Exception e) {
                result = e.Message.ToString ();
            }
            return result;
        }

        public bool UpdatePassword (Member member, string newPassword) {
            try {
                member.m_password = newPassword;
                db.SaveChanges ();
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        public bool updateMemberImgUrl (int Id) {
            var updateMember = db.Member.Where (m => m.MemberId == Id).FirstOrDefault ();
            updateMember.m_imgUrl = "../../../Back-End/File/UserImg/" + Id + ".jpg";
            db.SaveChanges ();
            return true;
        }

        public bool ApplyResAdmin (Application apply) {
            try {
                db.Application.Add (apply);
                db.SaveChanges ();
                return true;
            } catch (Exception e) {
                return false;
            }
        }
    }
}