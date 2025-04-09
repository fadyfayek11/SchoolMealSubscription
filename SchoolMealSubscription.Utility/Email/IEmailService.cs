namespace SchoolMealSubscription.Services.Email;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string message, byte[]? attachment = null, string attachmentName = null);
}