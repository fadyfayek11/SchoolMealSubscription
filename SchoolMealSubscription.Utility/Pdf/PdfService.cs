using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SchoolMealSubscription.Models.Entities;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SchoolMealSubscription.Services.Pdf;

public class PdfService : IPdfService
{
    public PdfService()
    {
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public byte[] GenerateOrderPdf(Orders order, ApplicationUser parent, Student student)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontFamily("Arial").FontSize(12));

                page.Header().Element(ComposeHeader);

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(column =>
                    {
                        // Invoice Details
                        column.Item()
                            .Background(Colors.Grey.Lighten5)
                            .Padding(10)
                            .BorderColor(Colors.Grey.Medium)
                            .Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.ConstantColumn(150);
                                });

                                table.Cell().AlignLeft().Text(order.InvoiceNumber);
                                table.Cell().AlignRight().Text("رقم الفاتورة").Bold();

                                table.Cell().AlignLeft().Text(DateTime.Now.ToString("yyyy/MM/dd"));
                                table.Cell().AlignRight().Text("تاريخ الفاتورة").Bold();

                                table.Cell().AlignLeft().Text(order.Status.ToString());
                                table.Cell().AlignRight().Text("حالة الاشتراك").Bold();
                            });

                        // Parent Information
                        column.Item()
                            .PaddingTop(15)
                            .AlignCenter()
                            .Text("معلومات ولي الأمر")
                            .FontSize(16)
                            .Bold();

                        column.Item()
                            .PaddingTop(5)
                            .Background(Colors.Grey.Lighten5)
                            .Padding(10)
                            .BorderColor(Colors.Grey.Medium)
                            .Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.ConstantColumn(150);
                                });

                                table.Cell().AlignLeft().Text(parent.FullName);
                                table.Cell().AlignRight().Text("الاسم الكامل").Bold();

                                table.Cell().AlignLeft().Text(parent.Email);
                                table.Cell().AlignRight().Text("البريد الإلكتروني").Bold();

                                table.Cell().AlignLeft().Text(parent.PhoneNumber);
                                table.Cell().AlignRight().Text("رقم الجوال").Bold();
                            });

                        // Student Information
                        column.Item()
                            .PaddingTop(15)
                            .AlignCenter()
                            .Text("معلومات الطالب")
                            .FontSize(16)
                            .Bold();

                        column.Item()
                            .PaddingTop(5)
                            .Background(Colors.Grey.Lighten5)
                            .Padding(10)
                            .BorderColor(Colors.Grey.Medium)
                            .Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.ConstantColumn(150);
                                });

                                table.Cell().AlignLeft().Text(student.Name);
                                table.Cell().AlignRight().Text("اسم الطالب").Bold();

                                table.Cell().AlignLeft().Text(student.Gender.ToString());
                                table.Cell().AlignRight().Text("الجنس").Bold();

                                table.Cell().AlignLeft().Text(student.Grade?.Name ?? "");
                                table.Cell().AlignRight().Text("الصف الدراسي").Bold();

                                table.Cell().AlignLeft().Text(student.School?.Name ?? "");
                                table.Cell().AlignRight().Text("اسم المدرسة").Bold();

                                table.Cell().AlignLeft().Text(student.HasAllergy ? student.AllergyDetails : "لا يوجد");
                                table.Cell().AlignRight().Text("الحساسية").Bold();
                            });

                        // Subscription Details
                        column.Item()
                            .PaddingTop(15)
                            .AlignCenter()
                            .Text("تفاصيل الاشتراك")
                            .FontSize(16)
                            .Bold();

                        column.Item()
                            .PaddingTop(5)
                            .Background(Colors.Grey.Lighten5)
                            .Padding(10)
                            .BorderColor(Colors.Grey.Medium)
                            .Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.ConstantColumn(150);
                                });

                                table.Cell().AlignLeft().Text(order.Duration.ToString());
                                table.Cell().AlignRight().Text("مدة الاشتراك").Bold();

                                table.Cell().AlignLeft().Text(order.PaymentMethod.ToString());
                                table.Cell().AlignRight().Text("طريقة الدفع").Bold();

                                table.Cell().AlignLeft().Text(order.SubscriptionDate.ToString("yyyy/MM/dd"));
                                table.Cell().AlignRight().Text("تاريخ البدء").Bold();
                            });

                        // Food Preferences
                        if (order.Student.FoodPreferences.Any())
                        {
                            column.Item()
                                .PaddingTop(15)
                                .AlignRight()
                                .Text("الأطعمة المفضلة")
                                .FontSize(16)
                                .Bold();

                            column.Item()
                                .PaddingTop(5)
                                .Background(Colors.Grey.Lighten5)
                                .Padding(10)
                                .BorderColor(Colors.Grey.Medium)
                                .Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn();
                                    });

                                    foreach (var pref in order.Student.FoodPreferences)
                                    {
                                        table.Cell().AlignLeft().Text(pref.FoodType?.Price.ToString("C",CultureInfo.CreateSpecificCulture("ar-SA"))).Bold();
                                        table.Cell().AlignRight().Text(pref.FoodType?.Name).Bold();
                                    }
                                });
                        }

                        // Total Amount
                        column.Item()
                            .PaddingTop(20)
                            .Background(Colors.Grey.Lighten4)
                            .Padding(10)
                            .BorderColor(Colors.Grey.Medium)
                            .AlignRight()
                            .Text($"المبلغ الإجمالي: {order.Amount} ريال سعودي")
                            .FontSize(16)
                            .Bold();
                    });

                page.Footer().Element(ComposeFooter);
            });
        });

        using var stream = new MemoryStream();
        document.GeneratePdf(stream);
        return stream.ToArray();
    }

    private void ComposeHeader(IContainer container)
    {
        container.Column(column =>
        {
            // Logo and Company Name Row
            column.Item().Row(row =>
            {
               
                // Company Name and Invoice Title
                row.RelativeItem()
                    .AlignRight()
                    .Column(col =>
                    {
                        col.Item()
                            .Text("برنامج التغذية المدرسية")
                            .FontSize(20)
                            .Bold();

                        col.Item()
                            .Text("School Meal Subscription Program")
                            .FontSize(14);
                    });
            });

            // Invoice Title
            column.Item()
                .PaddingTop(10)
                .AlignCenter()
                .Text("فاتورة اشتراك برنامج غذائي")
                .FontSize(22)
                .Bold();

            // Divider
            column.Item()
                .PaddingTop(5)
                .LineHorizontal(1)
                .LineColor(Colors.Grey.Medium);
        });
    }

    private void ComposeFooter(IContainer container)
    {
        container.Column(column =>
        {
            // Divider
            column.Item()
                .LineHorizontal(1)
                .LineColor(Colors.Grey.Medium);

            // Footer content
            column.Item()
                .PaddingTop(10)
                .AlignCenter()
                .Text(text =>
                {
                    text.Span("شكراً لثقتكم بنا").Bold().FontSize(14);
                    text.EmptyLine();
                    text.Span("للاستفسار يرجى الاتصال على: 920000000");
                    text.EmptyLine();
                    text.Span("www.schoolmeals.sa | support@schoolmeals.sa");
                });

            // Page number
            column.Item()
                .AlignCenter()
                .PaddingTop(10)
                .Text(text =>
                {
                    text.CurrentPageNumber();
                    text.Span(" / ");
                    text.TotalPages();
                    
                });
        });
    }
}