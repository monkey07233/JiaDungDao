using Back_End.Interface;
using Back_End.Models;
namespace Back_End.Services {
    public class MemberService : IMemberService {
        private readonly IMemberRepo MemberRepo;
        public MemberService (IMemberRepo memberRepo) {
            this.MemberRepo = memberRepo;
        }
        public string Register (Member member) {
           return MemberRepo.Register (member);          
        }
    }
}