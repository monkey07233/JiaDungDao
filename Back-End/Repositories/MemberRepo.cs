using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public Member GetMemberByEmail (string email) {
            return db.Member.Where (m => m.m_email == email).FirstOrDefault ();
        }

        public string Register (Member member) {
            try {
                db.Member.Add (member);
                db.SaveChanges ();
                return "successed";
            } catch (Exception e) {
                return e.Message.ToString ();
            }
        }

        public string VerifyAccount (Member member) {
            try {
                member.validateCode = "";
                db.SaveChanges ();
                return "Verified";
            } catch (Exception e) {
                return e.Message.ToString ();
            }
        }

        public string EditMemberInformation (Member originMember, MemberInfo memberAfterEdit) {
            try {
                originMember.m_name = memberAfterEdit.m_name;
                originMember.m_address = memberAfterEdit.m_address;
                originMember.m_birthday = memberAfterEdit.m_birthday;
                originMember.m_email = memberAfterEdit.m_email;
                db.SaveChanges ();
                return "Update completed!";
            } catch (Exception e) {
                return e.Message.ToString ();
            }
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

        public bool UpdateMemberImgUrl (int Id) {
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
        public List<Member> GetAllMember () {
            try {
                var allMember = db.Member.Where (m => m.m_role != 2).OrderBy (m => m.MemberId).ToList ();
                return allMember;
            } catch (System.Exception) {
                return null;
            }
        }
        public Application GetApplyByAcc (string account) {
            return db.Application.Where (a => a.m_account == account && a.status != false).FirstOrDefault ();
        }

        public bool VerifyApplication (bool pass,  Member updateMember) {
            try {
                var application = GetApplyByAcc (updateMember.m_account);
                if (pass) { //審核通過
                    updateMember.m_role = 1;
                    application.status = true;
                } else
                    application.status = false;
                db.SaveChanges ();
                return true;
            } catch (Exception e) {
                Debug.WriteLine ("更新角色權限失敗");
                Debug.WriteLine (e.ToString ());
                return false;
            }
        }

        public bool BlockMember (Member blockMember) {
            try {
                blockMember.isBlock = true;
                db.SaveChanges ();
                return true;
            } catch (System.Exception) {
                return false;
            }

        }

        public List<Application> GetAllApplication () {
            try {
                return db.Application.ToList ();
            } catch (System.Exception) {
                return null;
            }
        }

        public bool UnblockMember(Member blockMember)
        {
            try {
                blockMember.isBlock = false;
                db.SaveChanges ();
                return true;
            } catch (System.Exception) {
                return false;
            }
        }
    }
}