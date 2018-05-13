using System.Net.Mail;

namespace MailClient.Smtp.Models 
{
    public class IncomingMailModel : MailMessage
    {
        public IncomingMailModel(string from, string to) : base(from, to) 
        {
            IsBodyHtml = true;
        }
    }
}
