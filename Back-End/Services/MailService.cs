using System.Collections.Generic;
using System.Net.Mail;
using Back_End.Interface;
using Back_End.Models;

namespace Back_End.Services
{
    public class MailService : IMailService
    {
        private readonly string _mailAccount="as851468567";
        private readonly string _mailPassword="as8514685670";

        public void SendMail(string toMail){
            SmtpClient smtp=new SmtpClient("smtp.gmail.com",587);
            smtp.Credentials=new System.Net.NetworkCredential(_mailAccount,_mailPassword);
            smtp.EnableSsl=true;
            MailMessage mail=new MailMessage();
            mail.From=new MailAddress("as851468567@gmail.com");
            mail.To.Add(toMail);
            mail.Subject="test Mail";
            mail.Body="<h1>123</h1>";
            mail.IsBodyHtml=true;
            smtp.Send(mail);
        }
    }
}