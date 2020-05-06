using System.ComponentModel.DataAnnotations;
namespace Back_End.Models {
    public class Application {
        [Key]
        public int ApplicationID { get; set; }
        [Required]
        public string m_account { get; set; }
        [Required]
        [MaxLength (300)]
        public string reason { get; set; }
        [Required]
        public bool status { get; set; }
    }
}