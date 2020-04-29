using System.ComponentModel.DataAnnotations;

namespace Back_End.Models
{
    public class Restaurant
    {
        [Key]
        public int RestaurantID { get; set; }
        [Required]
        public string r_name { get; set; }
        public string r_address { get; set; }
        [MaxLength(10)]
        public string r_tel { get; set; }
        [Required]
        [MaxLength(50)]
        public string m_account { get; set; }
        public string r_imgUrl { get; set; }
    }
}