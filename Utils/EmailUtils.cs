using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Net.Teirlinck.Utils
{
    public static class EmailUtils
    {
        private static string smtpAddress = "smtp.gmail.com";
        private static int portNumber = 587;
        private static bool enableSSL = true;

        public static void SendEmailFromFXTrader(string subject, string body, IEnumerable<string> attachments = null)
        {
            string emailFrom = "fxtrader@teirlinck.net";
            string password = "FAKanYRSYY";
            string emailTo = "fxtrader@teirlinck.net";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                if (attachments != null)
                {
                    foreach (string attachment in attachments)
                        mail.Attachments.Add(new Attachment(attachment));
                }

                using (SmtpClient smtpClient = new SmtpClient(smtpAddress, portNumber))
                {
                    smtpClient.Credentials = new NetworkCredential(emailFrom, password);
                    smtpClient.EnableSsl = enableSSL;
                    smtpClient.Send(mail);
                }
            }
        }
    }
}
