using System;
using System.Collections.Generic;
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

        public string VerifyAccount (string account) {
            var member = MemberRepo.GetMemberByAcc (account);
            if (member == null) {
                return "查無此會員";
            }
            return MemberRepo.VerifyAccount (member);
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

        public bool ResetPassword (UpdateMemberInfo memberInfo) {
            var member = MemberRepo.GetMemberByAcc (memberInfo.m_account);
            if (member != null) {
                var hash_new_password = HashPassword (memberInfo.new_password);
                return MemberRepo.UpdatePassword (member, hash_new_password);;
            }
            return false;
        }

        public bool UpdatePassword (UpdateMemberInfo memberInfo) {
            var member = MemberRepo.GetMemberByAcc (memberInfo.m_account);
            if (member != null) {
                var hash_m_password = HashPassword (memberInfo.m_password);
                if (hash_m_password == member.m_password) {
                    var hash_new_password = HashPassword (memberInfo.new_password);
                    return MemberRepo.UpdatePassword (member, hash_new_password);
                }
                return false;
            }
            return false;
        }

        public bool updateMemberImgUrl (int Id) {
            var result = MemberRepo.updateMemberImgUrl (Id);
            return result;
        }

        public bool ApplyResAdmin (Application apply) {
            return MemberRepo.ApplyResAdmin (apply);
        }
        public List<Member> GetAllMember () {
            var result = MemberRepo.GetAllMember ();
            return result;
        }

        public bool VerifyApplication(bool pass,int appicationId)
        {
            return MemberRepo.VerifyApplication(pass,appicationId);
        }
    }
}