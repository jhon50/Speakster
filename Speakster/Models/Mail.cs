using System;
using System.Net.Mail;
using System.Net.Configuration;
using System.Configuration;
using System.Reflection;
using System.Net.Mime;
using System.Threading.Tasks;

public class Email
{
    public async Task Send(string to, string name, string subject, string text2, string html2 )
    {
        //try
        //{
            SmtpSection smtpSec = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            MailMessage mailMsg = new MailMessage();

            // To
            mailMsg.To.Add(new MailAddress(to, name));

            // From
            mailMsg.From = new MailAddress(smtpSec.From, "Speakster");

            // Subject and multipart/alternative Body
            mailMsg.Subject = subject;
            string text = text2;
            string html = html2;
            //string html = @"<p>html body</p>";
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            // Init SmtpClient and send
            SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpSec.Network.UserName, smtpSec.Network.Password);
            smtpClient.Credentials = credentials;

        await smtpClient.SendMailAsync(mailMsg);
        //await smtpClient.SendMailAsync(mailMsg);
            //return true;
        }
        //catch (Exception ex)
        //{
        //    Console.WriteLine(ex.Message);
        //    return false;
        //}
    
}