using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace SchoolMealSubscription.Services.Email;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var smtpSettings = _configuration.GetSection("SmtpSettings");
        var host = smtpSettings["Host"];
        var port = int.Parse(smtpSettings["Port"]);
        var enableSsl = bool.Parse(smtpSettings["EnableSsl"]);
        var userName = smtpSettings["UserName"];
        var password = smtpSettings["Password"];
        var fromEmail = smtpSettings["FromEmail"];
        var fromName = smtpSettings["FromName"];

        using var client = new SmtpClient(host, port)
        {
            Credentials = new NetworkCredential(userName, password),
            EnableSsl = enableSsl
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(fromEmail, fromName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(toEmail);

        try
        {
            await client.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            // Log the exception and rethrow
            Console.WriteLine($"Error sending email: {ex.Message}");
            throw;
        }
    }
}