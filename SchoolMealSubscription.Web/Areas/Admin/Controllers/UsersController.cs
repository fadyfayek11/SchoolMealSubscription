using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolMealSubscription.DataAccess.Data;
using SchoolMealSubscription.Services;

namespace SchoolMealSubscription.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Roles.Role_Admin)]

public class UsersController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly ApplicationDbContext _context;

    public UsersController(ApplicationDbContext context)
    {
        _context = context;
    }
    // GET: Students
    public async Task<IActionResult> Index()
    {
        var students = await _context.Students
            .Include(s => s.Grade)
            .Include(s => s.School)
            .Include(s => s.Parent)
            .OrderBy(x=>x.Name)
            .ToListAsync();

        return View(students);
    }
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students
            .Include(s => s.Grade)
            .Include(s => s.School)
            .Include(s => s.FoodPreferences)
            .ThenInclude(fp => fp.FoodType)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }
}