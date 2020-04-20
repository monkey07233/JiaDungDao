using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Back_End.Interface;
using Back_End.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Back_End.Services {
    public class MemberService : IMemberService {
        private readonly IMemberRepo MemberRepo;

        public MemberService (IMemberRepo memberRepo) {
            this.MemberRepo = memberRepo;
        }

        public async Task<Member> GetMemberByLogin (string m_account, string m_password) {
            var hash_m_password = HashPassword (m_password);
            var result = await MemberRepo.GetMemberByLogin (m_account, hash_m_password);
            return result;
        }

        public string HashPassword (string password) {
            string result = string.Empty;
            string saltKey = "1a2b3d4c5e6f7g8h9i";
            string saltAndPassword = string.Concat (password, saltKey);
            byte[] saltAndPasswordByte = Encoding.UTF8.GetBytes (saltAndPassword);
            SHA512CryptoServiceProvider sHA512 = new SHA512CryptoServiceProvider ();
            byte[] hashResult = sHA512.ComputeHash (saltAndPasswordByte);
            result = Convert.ToBase64String (hashResult);
            return result;
        }

        public string GetJwtToken (IConfiguration configuration, string MemberId, string m_account) {
            var claims = new [] {
                new Claim (JwtRegisteredClaimNames.NameId, m_account),
                new Claim (JwtRegisteredClaimNames.AuthTime, DateTime.Now.ToString ()),
                new Claim (JwtRegisteredClaimNames.UniqueName, MemberId)
            };
            var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (configuration["JWT:key"]));
            var credentials = new SigningCredentials (key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken (
                issuer: configuration["JWT:issuer"],
                audience : configuration["JWT:audience"],
                signingCredentials : credentials,
                expires : DateTime.Now.AddMinutes (120),
                claims : claims
            );
            return new JwtSecurityTokenHandler ().WriteToken (jwtToken);
        }

        public string Register (Member member) {
            var getMember = MemberRepo.GetMemberByAcc (member.m_account);
            if (getMember != null) {
                return "帳號已存在";
            }
            member.m_password = HashPassword (member.m_password);
            return MemberRepo.Register (member);
        }

        public Member GetMemberByAcc (string account) {
            return MemberRepo.GetMemberByAcc (account);
        }

        public string EditMemberInformation (UpdateMemberInfo memberAfterEdit) {
            var originMember = MemberRepo.GetMemberByAcc (memberAfterEdit.m_account);
            if (originMember != null) {
                var result = MemberRepo.EditMemberInformation (originMember, memberAfterEdit);
                return result;
            }
            return "查無此會員";
        }
    }
}