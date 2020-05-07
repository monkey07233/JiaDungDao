using System;

namespace Back_End.Models {
    public class MemberInfo {
        public string m_name { get; set; }
        public string m_email { get; set; }
        public string m_account { get; set; }
        public DateTime m_birthday { get; set; }
        public string m_address { get; set; }
        public string m_password { get; set; }
        public string validateCode { get; set; }
        public string new_password { get; set; }
    }
}