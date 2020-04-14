using System;
using System.ComponentModel.DataAnnotations;

namespace Back_End.Models
{
    public class Member
    {
        [Key]
        public int MemberId{get;set;}
        
        [Required]
        [MaxLength(50)]
        public string m_name{get;set;}
        [Required]
        [MaxLength(250)]
        public string m_email{get;set;}
        [Required]
        [MaxLength(50)]
        public string m_account{get;set;}
        [Required]
        public string m_password{get;set;}
        [Required]
        public DateTime m_birthday{get;set;}
        [Required]
        public string m_address{get;set;}
    }
}