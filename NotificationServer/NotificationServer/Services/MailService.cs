using MailKit.Net.Smtp;
using MimeKit;
using NotificationServer.Configurations;
using Shared.Dtos.Emails;
using NotificationServer.Templates;

namespace NotificationServer.Services;

public class MailService : IMailService
{
    #region Constructor
    private readonly IConfiguration _configuration;

    public MailService(IConfiguration configuration)
    {
        this._configuration = configuration;
    } 
    #endregion

    public async Task SendEmailAsync(EmailBodyDto email)
    {
        var configuration = _configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(configuration.DisplayName, configuration.From));
        emailMessage.To.Add(new MailboxAddress("Gozel", email.To));

        if (!string.IsNullOrEmpty(email.Cc)) 
        {
            emailMessage.Cc.Add(new MailboxAddress("Shab cc", email.Cc));
        }

        if (!string.IsNullOrEmpty(email.Bcc))
        {
            emailMessage.Bcc.Add(new MailboxAddress("Kamal cc", email.Bcc));
        }

        emailMessage.Subject = email.Subject;
        var bodyBuilder = new BodyBuilder()
        {
            HtmlBody = email.Body.Info()
        };

        if (email.Attachments.Count > 0)
        {
            foreach (var attachment in email.Attachments)
            {
                bodyBuilder.Attachments.Add(attachment.FileName, attachment.FileContent);
            }
        }

        emailMessage.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            try
            {
                await client.ConnectAsync(configuration.SmtpServer, configuration.Port, false);
                await client.AuthenticateAsync(configuration.Username, configuration.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
