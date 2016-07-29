using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SendGrid;
using System.Net;
using System.Configuration;
using System.Diagnostics;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net.Mime;

namespace Speakster.Models
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await configSendGridasync(message);
        }

        // Use NuGet to install SendGrid (Basic C# client lib) 
        private async Task configSendGridasync(IdentityMessage message)
        {

            MailMessage mailMsg = new MailMessage();

            // To
            mailMsg.To.Add(new MailAddress("johnnlds2@gmail.com", "To Name"));

            // From
            mailMsg.From = new MailAddress("john@speakster.com.br", "From Name");

            // Subject and multipart/alternative Body
            mailMsg.Subject = "subject";
            string text = "text body";
            string html = @"<p>html body</p>";
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            // Init SmtpClient and send
            SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("speakster", "16cj1CKNkj2r1");
            smtpClient.Credentials = credentials;

            await smtpClient.SendMailAsync(mailMsg);
        }
    }
}