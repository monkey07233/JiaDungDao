using Microsoft.AspNetCore.Http;
namespace Back_End.Models {
    public class UploadInfo {
        public int id { get; set; }
        public int uploadType { get; set; }
        public IFormFile files { get; set; }
    }
}