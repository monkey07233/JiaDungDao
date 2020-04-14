using System;
using System.Linq;
using Back_End.Interface;
using Back_End.Models;
using JiaDungDao.Connection;

namespace Back_End.Repositories {
    public class MemberRepo : IMemberRepo {
        private readonly ApplicationDbContext db;
        public MemberRepo (ApplicationDbContext dbContext) {
            this.db = dbContext;
        }

        public string Register (Member member) {
            string result = "";
            var m = db.Member.Where (m => m.m_account == member.m_account).FirstOrDefault ();
            if (m == null) {
                try {
                    db.Member.Add (member);
                    db.SaveChanges ();
                    result = "successed";
                } catch (Exception e) {
                    result = e.Message.ToString ();
                }
                return result;
            } else {                 
                return "帳號已存在";
            }
        }
    }
}