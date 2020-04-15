using System.Text;
using Back_End.Interface;
using Back_End.Models;
using System.Security.Cryptography;
using System;

namespace Back_End.Services 
{
    public class MemberService : IMemberService 
    {
        private readonly IMemberRepo MemberRepo;

        public MemberService (IMemberRepo memberRepo) {
            this.MemberRepo = memberRepo;
        }

        public Member GetMemberByLogin(Member member)
        {
            member.m_password = HashPassword(member.m_password);
            var result = MemberRepo.GetMemberByLogin(member);
            return result;
        }

        public string HashPassword(string password)
        {
            string result = string.Empty;
            string saltKey = "1a2b3d4c5e6f7g8h9i";
            string saltAndPassword = string.Concat(password,saltKey);
            byte[] saltAndPasswordByte = Encoding.UTF8.GetBytes(saltAndPassword);
            SHA512CryptoServiceProvider sHA512 = new SHA512CryptoServiceProvider();
            byte[] hashResult = sHA512.ComputeHash(saltAndPasswordByte);
            result = Convert.ToBase64String(hashResult);
            return result;
        }

        public string Register (Member member) {
            return MemberRepo.Register (member);
        }
    }
}