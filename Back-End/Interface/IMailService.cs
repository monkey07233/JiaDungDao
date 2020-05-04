namespace Back_End.Interface
{
    public interface IMailService
    {
        void SendMail(string toMail,string account);
        bool SendResetPasswordMail(string toMail,string account);
    }
}