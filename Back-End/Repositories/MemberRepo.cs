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
            var result = await db.Member.Where (m => m.m_account == m_account && m.m_password == hash_m_password).FirstOrDefaultAsync ();
            if (result != null) {
                return result;
            }
            return null;
        }

        public Member isAccountExist (string m_account) {
            var m = db.Member.Where (m => m.m_account == m_account).FirstOrDefault ();
            return m;
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

        public Member GetMemberInformation (int MemberId) {
            var result = db.Member.Where (m => m.MemberId == MemberId).FirstOrDefault ();
            if (result != null) {
                return result;
            } else {
                return null;
            }
        }
    }
}