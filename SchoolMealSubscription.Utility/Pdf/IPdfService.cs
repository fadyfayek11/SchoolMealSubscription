using SchoolMealSubscription.Models.Entities;

namespace SchoolMealSubscription.Services.Pdf;

public interface IPdfService
{
    byte[] GenerateOrderPdf(Orders order, ApplicationUser parent, Student student);
}