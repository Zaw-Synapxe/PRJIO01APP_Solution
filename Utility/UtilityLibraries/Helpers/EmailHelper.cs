using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UtilityLibraries.Helpers
{
    public class EmailHelper : IDisposable
    {
        public async Task SendMailAsync(string from, string displayName, string to, string subject, string body, int port, string host, string key)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(from, displayName);
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                message.IsBodyHtml = false;
                message.Body = body;
                smtp.Port = port;
                smtp.Host = host;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(from, key);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                await smtp.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public void Dispose()
        {
        }
    }
}
