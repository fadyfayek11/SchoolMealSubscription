using System.Net;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using MimeKit;
using MailKit.Net.Smtp;  
using MailKit.Security;
using System.Net;

using System.Net.Mail;
using SmtpClient = System.Net.Mail.SmtpClient;

namespace SchoolMealSubscription.Services.Email;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task SendEmailAsync(string email, string subject, string message, byte[] attachment = null, string attachmentName = null)
    {
        var emailSettings = _configuration.GetSection("SmtpSettings");
        var smtpServer = emailSettings["SmtpServer"];
        var smtpPort = int.Parse(emailSettings["SmtpPort"]);
        var smtpUsername = emailSettings["SmtpUsername"];
        var smtpPassword = emailSettings["SmtpPassword"];
        var fromAddress = emailSettings["FromAddress"];
        var fromName = emailSettings["FromName"];

        using var mailMessage = new MailMessage
        {
            From = new MailAddress(fromAddress, fromName),
            Subject = subject,
            Body = message,
            IsBodyHtml = true
        };

        mailMessage.To.Add("dyoomi2011@hotmail.com");

        // Handle the attachment differently
        if (attachment != null && attachmentName != null)
        {
            var contentType = new System.Net.Mime.ContentType();
            contentType.MediaType = System.Net.Mime.MediaTypeNames.Application.Octet;
            contentType.Name = attachmentName;

            mailMessage.Attachments.Add(new Attachment(new MemoryStream(attachment), contentType));
        }

        using var smtpClient = new SmtpClient(smtpServer, smtpPort)
        {
            Credentials = new NetworkCredential(smtpUsername, smtpPassword),
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false
        };

        await smtpClient.SendMailAsync(mailMessage);
    }
}