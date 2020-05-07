using System.Net.Mail;
using Back_End.Interface;

namespace Back_End.Services {
    public class MailService : IMailService {
        private readonly string _mailAccount = "jiadungdao";
        private readonly string _mailPassword = "zzrjgiquewjrfqwp";
        public bool SendMail (string toMail, string account, string subject, string body) {
            try {
                SmtpClient smtp = new SmtpClient ("smtp.gmail.com", 587);
                smtp.Credentials = new System.Net.NetworkCredential (_mailAccount, _mailPassword);
                smtp.EnableSsl = true;
                MailMessage mail = new MailMessage ();
                mail.From = new MailAddress ("jiadungdao@gmail.com");
                mail.To.Add (toMail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                smtp.Send (mail);
                return true;
            } catch (System.Exception e) {
                return false;
            }
        }
        public string GetMailBody (string account, string bodyType, string validateCode = "") {
            string url = string.Empty;
            string mailBody = string.Empty;
            switch (bodyType) {
                case "reset":
                    url = "http://localhost:8080/ResetPassword?account=" + account;
                    mailBody = System.IO.File.ReadAllText ("ResetPasswordMail.html");
                    mailBody = mailBody.Replace ("resetPasswordUrl", url);
                    break;
                case "validate":
                    url = "http://localhost:8080/Validate?account=" + account + "&validateCode=" + validateCode;
                    mailBody = System.IO.File.ReadAllText ("verficationMail.html");
                    mailBody = mailBody.Replace ("validateUrl", url);
                    break;
            }
            return mailBody;
        }
    }
}