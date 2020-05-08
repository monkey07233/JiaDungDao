using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
        private readonly IMemberRepo _memberRepo;

        public MemberService (IMemberRepo memberRepo) {
            this._memberRepo = memberRepo;

        }

        public async Task<Member> GetMemberByLogin (string m_account, string m_password) {
            var hash_m_password = HashPassword (m_password);
            var result = await _memberRepo.GetMemberByLogin (m_account, hash_m_password);
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
            var getMember = _memberRepo.GetMemberByAcc (member.m_account);
            if (getMember != null) {
                return "帳號已存在";
            }
            var getMemberByEmail = _memberRepo.GetMemberByEmail (member.m_email);
            if (getMemberByEmail != null) {
                return "此信箱已註冊過";
            }
            member.m_password = HashPassword (member.m_password);
            member.validateCode = CreateValidateCode ();
            return _memberRepo.Register (member);
        }

        public string VerifyAccount (string account, string validateCode) {
            var member = _memberRepo.GetMemberByAcc (account);
            if (member != null) {
                if (member.validateCode == validateCode) {
                    return _memberRepo.VerifyAccount (member);
                }
            }
            return "查無此會員";
        }

        public Member GetMemberByAcc (string account) {
            return _memberRepo.GetMemberByAcc (account);
        }

        public string EditMemberInformation (MemberInfo memberAfterEdit) {
            var originMember = _memberRepo.GetMemberByAcc (memberAfterEdit.m_account);
            if (originMember != null) {
                return _memberRepo.EditMemberInformation (originMember, memberAfterEdit);
            }
            return "查無此會員";
        }

        public bool ResetPassword (MemberInfo memberInfo) {
            var member = _memberRepo.GetMemberByAcc (memberInfo.m_account);
            if (member != null) {
                var hash_new_password = HashPassword (memberInfo.new_password);
                return _memberRepo.UpdatePassword (member, hash_new_password);;
            }
            return false;
        }

        public bool UpdatePassword (MemberInfo memberInfo) {
            var member = _memberRepo.GetMemberByAcc (memberInfo.m_account);
            if (member != null) {
                var hash_m_password = HashPassword (memberInfo.m_password);
                if (hash_m_password == member.m_password) {
                    var hash_new_password = HashPassword (memberInfo.new_password);
                    return _memberRepo.UpdatePassword (member, hash_new_password);
                }
                return false;
            }
            return false;
        }

        public bool UpdateMemberImgUrl (int Id) {
            return _memberRepo.UpdateMemberImgUrl (Id);
        }

        public bool ApplyResAdmin (Application apply) {
            var application = _memberRepo.GetApplyByAcc (apply.m_account);
            if (application == null)
                return _memberRepo.ApplyResAdmin (apply);
            else
                return false;
        }
        public List<Member> GetAllMember () {
            return _memberRepo.GetAllMember ();
        }

        public bool VerifyApplication (bool pass, string account) {
            var updateMember = _memberRepo.GetMemberByAcc(account);
            return _memberRepo.VerifyApplication (pass, account,updateMember);
        }

        public bool BlockMember (string m_account) {
            var blockMember = _memberRepo.GetMemberByAcc (m_account);
            if (blockMember != null) {
                return _memberRepo.BlockMember (blockMember);
            }
            return false;
        }
        public List<Application> GetAllApplication () {
            return _memberRepo.GetAllApplication ();
        }

        public string CreateValidateCode () {
            string result = string.Empty;
            string[] code = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            Random random = new Random ();
            for (int i = 0; i < 10; i++) {
                result += code[random.Next (code.Count ())];
            }
            return result;
        }
    }
}