using Microsoft.AspNetCore.Identity.UI.Services;

namespace SchoolMealSubscription.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //logic Send email
            return Task.CompletedTask;
        }
    }
}
