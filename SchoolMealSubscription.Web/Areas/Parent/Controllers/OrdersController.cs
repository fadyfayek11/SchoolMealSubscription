﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolMealSubscription.DataAccess.Data;
using SchoolMealSubscription.Models.Entities;
using SchoolMealSubscription.Models.Enums;
using SchoolMealSubscription.Models.ViewModels;
using SchoolMealSubscription.Services;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SchoolMealSubscription.Services.Pdf;
using SchoolMealSubscription.Services.Email;

namespace SchoolMealSubscription.Web.Areas.Parent.Controllers;
[Area("Parent")]
[Authorize(Roles = Roles.Role_Parent)]
public class OrdersController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPdfService _pdfService;
    private readonly IEmailService _emailService;

    public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IPdfService pdfService, IEmailService emailService)
    {
        _context = context;
        _userManager = userManager;
        _pdfService = pdfService;
        _emailService = emailService;
    }

    // GET: Orders/Create
    public async Task<IActionResult> Create()
    {
        var parentId = _userManager.GetUserId(User);

        var model = new OrderViewModel
        {
            Students = await _context.Students
                .Where(s => s.ParentId == parentId)
                .Include(s => s.School)
                .Include(s => s.Grade)
                .ToListAsync(),
            FoodPreferences = await _context.FoodTypes.ToListAsync()
        };

        return View(model);
    }

    // POST: Orders/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OrderViewModel model)
    {
        if (ModelState.IsValid)
        {
            var student = await _context.Students
                .Include(s => s.FoodPreferences)
                .ThenInclude(fp => fp.FoodType)
                .FirstOrDefaultAsync(s => s.Id == model.StudentId);

            if (student == null)
            {
                ModelState.AddModelError("", "Student not found");
                return View(model);
            }

            var order = new Orders
            {
                ParentId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                StudentId = model.StudentId,
                Duration = model.Duration,
                PaymentMethod = model.PaymentMethod,
                AdditionalNotes = model.AdditionalNotes,
                Status = SubscriptionStatus.Pending,
                SubscriptionDate = DateTime.Now,
                InvoiceNumber = GenerateInvoiceNumber(),
                Amount = CalculateAmount(model.Duration, student.FoodPreferences.Select(fp => fp.FoodType.Price).Sum())
            };

            _context.Add(order);
            await _context.SaveChangesAsync();

            var parent = await _userManager.FindByIdAsync(order.ParentId);

            // Generate PDF invoice
            var pdfBytes = _pdfService.GenerateOrderPdf(order, parent, student);

            // Send email with PDF attachment
            var emailSubject = $"فاتورة اشتراك برنامج غذائي - {order.InvoiceNumber}";
            var emailBody = $@"
                    <h2>مرحباً {parent.FullName},</h2>
                    <p>شكراً لتسجيلك في برنامجنا الغذائي. تم استلام طلبك بنجاح وسيتم مراجعته من قبل إدارتنا.</p>
                    <p>تفاصيل الطلب:</p>
                    <ul>
                        <li>رقم الفاتورة: {order.InvoiceNumber}</li>
                        <li>اسم الطالب: {student.Name}</li>
                        <li>مدة الاشتراك: {order.Duration.ToString()}</li>
                        <li>المبلغ: {order.Amount} ريال</li>
                    </ul>
                    <p>ستجدون الفاتورة مرفقة مع هذا البريد.</p>
                    <p>لأي استفسار، لا تتردد في التواصل معنا.</p>
                    <p>مع خالص التقدير,</p>
                    <p>فريق البرنامج الغذائي</p>";

            await _emailService.SendEmailAsync(
                parent.Email,
                emailSubject,
                emailBody,
                pdfBytes,
                $"فاتورة_{order.InvoiceNumber}.pdf");

            return RedirectToAction("Details", new { id = order.Id });
        }

        // Reload data if validation fails
        model.Students = await _context.Students
            .Where(s => s.ParentId == User.FindFirstValue(ClaimTypes.NameIdentifier))
            .ToListAsync();
        model.FoodPreferences = await _context.FoodTypes.ToListAsync();

        return View(model);
    }

    // GET: Orders/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var order = await _context.Orders
            .Include(o => o.Student)
            .ThenInclude(s => s.Grade)
            .Include(o => o.Student)
            .ThenInclude(s => s.School)
            .Include(o => o.Student)
            .ThenInclude(s => s.FoodPreferences)
            .ThenInclude(fp => fp.FoodType)
            .Include(o => o.Parent)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (order == null)
        {
            return NotFound();
        }

        // Verify the current user is the parent of this order
        if (order.ParentId != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return Forbid();
        }

        return View(order);
    }

    // GET: Orders
    public async Task<IActionResult> Index()
    {
        var parentId = _userManager.GetUserId(User);

        var orders = await _context.Orders
            .Include(o => o.Student)
            .Where(o => o.ParentId == parentId)
            .OrderByDescending(o => o.SubscriptionDate)
            .ToListAsync();

        return View(orders);
    }

    private string GenerateInvoiceNumber()
    {
        return $"INV-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
    }

    private decimal CalculateAmount(SubscriptionDuration duration, decimal dailyPrice)
    {
        return duration switch
        {
            SubscriptionDuration.OneMonth => dailyPrice * 30,
            SubscriptionDuration.Semester => dailyPrice * 120,
            SubscriptionDuration.FullYear => dailyPrice * 365,
            _ => dailyPrice * 30
        };
    }

    [HttpGet]
    public async Task<IActionResult> GetStudentDetails(int id)
    {
        var student = await _context.Students
            .Include(s => s.School)
            .Include(s => s.Grade)
            .Include(s => s.FoodPreferences)
            .ThenInclude(fp => fp.FoodType)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (student == null)
        {
            return NotFound();
        }

        return PartialView("_StudentDetailsPartial", student);
    }

    [HttpGet]
    public async Task<IActionResult> GetOrderSummary(int studentId, SubscriptionDuration duration)
    {
        var student = await _context.Students
            .Include(s => s.FoodPreferences)
            .ThenInclude(fp => fp.FoodType)
            .FirstOrDefaultAsync(s => s.Id == studentId);

        if (student == null)
        {
            return NotFound();
        }

        var dailyPrice = student.FoodPreferences.Sum(fp => fp.FoodType.Price);
        var totalAmount = CalculateAmount(duration, dailyPrice);

        ViewBag.Duration = duration;
        ViewBag.DailyPrice = dailyPrice;
        ViewBag.TotalAmount = totalAmount;

        return PartialView("_OrderSummaryPartial", student);
    }
    
    public async Task<IActionResult> DownloadInvoice(int id)
    {
        var order = await _context.Orders
            .Include(o => o.Parent)
            .Include(s => s.Student)
            .ThenInclude(o => o.FoodPreferences)
            .ThenInclude(o => o.FoodType)
            .Include(s => s.Student)
            .ThenInclude(s => s.School)
            .Include(s => s.Student)
            .ThenInclude(s => s.Grade)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
        {
            return NotFound();
        }

        var pdfBytes = _pdfService.GenerateOrderPdf(order, order.Parent, order.Student);
        return File(pdfBytes, "application/pdf", $"Invoice_{order.InvoiceNumber}.pdf");
    }
}
