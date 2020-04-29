using System.Net.Mail;
using Back_End.Interface;

namespace Back_End.Services
{
    public class MailService : IMailService
    {
        private readonly string _mailAccount="jiadungdao";
        private readonly string _mailPassword="zzrjgiquewjrfqwp";

        public void SendMail(string toMail,string account){
            SmtpClient smtp=new SmtpClient("smtp.gmail.com",587);
            smtp.Credentials=new System.Net.NetworkCredential(_mailAccount,_mailPassword);
            smtp.EnableSsl=true;
            MailMessage mail=new MailMessage();
            mail.From=new MailAddress("jiadungdao@gmail.com");
            mail.To.Add(toMail);
            mail.Subject="呷午餐會員認證";
            mail.Body=GetMailBody(account);
            mail.IsBodyHtml=true;
            smtp.Send(mail);
        }

        private string GetMailBody(string account)
        {
            string validateUrl="http://localhost:8080/validate?account="+account;
            string mailBody=System.IO.File.ReadAllText("verficationMail.html");
            mailBody=mailBody.Replace("validateUrl",validateUrl);
            return mailBody;
        }
    }
}