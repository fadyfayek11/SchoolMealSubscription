using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolMealSubscription.DataAccess.Data;
using SchoolMealSubscription.Models.Entities;
using SchoolMealSubscription.Models.Enums;
using SchoolMealSubscription.Services;

namespace SchoolMealSubscription.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Roles.Role_Admin)]
public class AdminOrdersController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly ApplicationDbContext _context;

    public AdminOrdersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: AdminOrders
    public async Task<IActionResult> Index()
    {
        var orders = await _context.Orders
            .Include(o => o.Parent)
            .Include(s => s.Student)
            .ThenInclude(o => o.FoodPreferences)
            .ThenInclude(fp => fp.FoodType)
            .OrderByDescending(o => o.SubscriptionDate)
            .ToListAsync();

        return View(orders);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id, SubscriptionStatus status)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        order.Status = status;
        _context.Update(order);
        await _context.SaveChangesAsync();

        return Json(new { success = true, newStatus = status.ToString() });
    }
}