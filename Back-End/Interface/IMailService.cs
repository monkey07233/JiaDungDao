namespace Back_End.Interface
{
    public interface IMailService
    {
        bool SendMail(string toMail,string account,string subject,string body);
        string GetMailBody(string account,string bodyType, string validateCode = "");
    }
}