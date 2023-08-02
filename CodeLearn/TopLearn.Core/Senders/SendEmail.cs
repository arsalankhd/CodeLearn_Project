using System.Net.Mail;

namespace CodeLearn.Core.Senders
{
    public class SendEmail
    {
        public static void Send(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("arsalan.teddy.rex@gmail.com", "کد لرن");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            //System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("arsalan.teddy.rex@gmail.com", "zxgzkggmxoxlfsjr");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }
    }
}