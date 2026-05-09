using Microsoft.Extensions.Options;
using PlayLib.Application.Interfaces;
using PlayLib.Data.Options;
using System.Net.Mail;

namespace PlayLib.Application.Services;

public class EmailSender(IOptions<GmailOptions> gmailOptions) : IEmailSender 
{
    private readonly GmailOptions _gmailOptions = gmailOptions.Value;

    public async void SendEmail(string recipient, string subject, string message)
    {
        var client = new SmtpClient(_gmailOptions.Host, _gmailOptions.Port)
        {
            Credentials = new System.Net.NetworkCredential(_gmailOptions.Email, _gmailOptions.Password),
            EnableSsl = true
        };

        var mailMessage = new MailMessage(_gmailOptions.Email, recipient, subject, message);

        await client.SendMailAsync(mailMessage);
    }
}
