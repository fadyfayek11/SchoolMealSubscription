using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolMealSubscription.DataAccess.Data;
using SchoolMealSubscription.Models.Entities;
using SchoolMealSubscription.Models.ViewModels;
using SchoolMealSubscription.Services;

namespace SchoolMealSubscription.Web.Areas.Parent.Controllers;

[Area("Parent")]
[Authorize(Roles = Roles.Role_Parent)]
public class ChildrenController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ChildrenController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Students
    public async Task<IActionResult> Index()
    {
        var parentId = _userManager.GetUserId(User);
        var children = await _context.Students
            .Include(s => s.Grade)
            .Include(s => s.School)
            .Where(s => s.ParentId == parentId)
            .ToListAsync();

        return View(children);
    }

    // GET: Students/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var parentId = _userManager.GetUserId(User);
        var student = await _context.Students
            .Include(s => s.Grade)
            .Include(s => s.School)
            .Include(s => s.FoodPreferences)
            .ThenInclude(fp => fp.FoodType)
            .FirstOrDefaultAsync(m => m.Id == id && m.ParentId == parentId);

        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    // GET: Students/Create
    public async Task<IActionResult> Create()
    {
        var model = new StudentViewModel
        {
            Grades = await _context.Grades.Select(g => new SelectListItem
            {
                Value = g.GradeId.ToString(),
                Text = g.Name
            }).ToListAsync(),
            Schools = await _context.Schools.Select(s => new SelectListItem
            {
                Value = s.SchoolId.ToString(),
                Text = s.Name
            }).ToListAsync(),
            AvailableFoodTypes = await _context.FoodTypes
                .Select(ft => new FoodTypeOption
                {
                    Id = ft.FoodTypeId,
                    Name = ft.Name!,
                }).ToListAsync()
        };

        return View("CreateOrEdit", model);
    }

    // POST: Students/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StudentViewModel model)
    {
        if (ModelState.IsValid)
        {
            var parentId = _userManager.GetUserId(User);

            var student = new Student
            {
                Name = model.Name,
                Gender = model.Gender,
                GradeId = model.GradeId,
                SchoolId = model.SchoolId,
                HasAllergy = model.HasAllergy,
                AllergyDetails = model.AllergyDetails ?? string.Empty,
                ParentId = parentId,
                FoodPreferences = model.SelectedFoodTypeIds.Select(id => new StudentFoodPreference
                {
                    FoodTypeId = id
                }).ToList()
            };

            _context.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Repopulate dropdowns if model is invalid
        model.Grades = await _context.Grades.Select(g => new SelectListItem
        {
            Value = g.GradeId.ToString(),
            Text = g.Name
        }).ToListAsync();

        model.Schools = await _context.Schools.Select(s => new SelectListItem
        {
            Value = s.SchoolId.ToString(),
            Text = s.Name
        }).ToListAsync();
        model.AvailableFoodTypes = await GetAvailableFoodTypes();

        return View("CreateOrEdit", model);
    }

    // GET: Students/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var parentId = _userManager.GetUserId(User);
        var student = await _context.Students
            .Include(s => s.FoodPreferences).ThenInclude(s=>s.FoodType)
            .FirstOrDefaultAsync(s => s.Id == id && s.ParentId == parentId);

        if (student == null)
        {
            return NotFound();
        }
        var availableFoodTypes = await GetAvailableFoodTypes();
        var selectedIds = student.FoodPreferences?.Select(fp => fp.FoodTypeId).ToList() ?? new List<int>();

        var model = new StudentViewModel
        {
            Id = student.Id,
            Name = student.Name!,
            Gender = student.Gender,
            GradeId = student.GradeId,
            SchoolId = student.SchoolId,
            HasAllergy = student.HasAllergy,
            AllergyDetails = student.AllergyDetails,
            Grades = await _context.Grades.Select(g => new SelectListItem
            {
                Value = g.GradeId.ToString(),
                Text = g.Name,
                Selected = g.GradeId == student.GradeId
            }).ToListAsync(),
            Schools = await _context.Schools.Select(s => new SelectListItem
            {
                Value = s.SchoolId.ToString(),
                Text = s.Name,
                Selected = s.SchoolId == student.SchoolId
            }).ToListAsync(),
            AvailableFoodTypes = availableFoodTypes.Select(ft => new FoodTypeOption
            {
                Id = ft.Id,
                Name = ft.Name,
                IsSelected = selectedIds.Contains(ft.Id)
            }).ToList(),
            SelectedFoodTypeIds = selectedIds
        };

        return View("CreateOrEdit", model);
    }

    // POST: Students/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, StudentViewModel model)
    {
        if (id != model.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var parentId = _userManager.GetUserId(User);
                var student = await _context.Students.Include(student => student.FoodPreferences)
                    .FirstOrDefaultAsync(s => s.Id == id && s.ParentId == parentId);

                if (student == null)
                {
                    return NotFound();
                }

                student.Name = model.Name;
                student.Gender = model.Gender;
                student.GradeId = model.GradeId;
                student.SchoolId = model.SchoolId;
                student.HasAllergy = model.HasAllergy;
                student.AllergyDetails = model.AllergyDetails;

                var existingPreferences = student.FoodPreferences ?? new List<StudentFoodPreference>();
                var selectedIds = model.SelectedFoodTypeIds ?? new List<int>();

                // Remove deselected preferences
                var toRemove = existingPreferences
                    .Where(fp => !selectedIds.Contains(fp.FoodTypeId))
                    .ToList();

                foreach (var item in toRemove)
                {
                    _context.Remove(item);
                }

                // Add new selections
                var existingIds = existingPreferences.Select(fp => fp.FoodTypeId).ToList();
                var toAdd = selectedIds
                    .Where(id => !existingIds.Contains(id))
                    .Select(id => new StudentFoodPreference
                    {
                        StudentId = student.Id,
                        FoodTypeId = id
                    });

                foreach (var item in toAdd)
                {
                    _context.Add(item);
                }


                _context.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(model.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // Repopulate dropdowns if model is invalid
        model.Grades = await _context.Grades.Select(g => new SelectListItem
        {
            Value = g.GradeId.ToString(),
            Text = g.Name
        }).ToListAsync();

        model.Schools = await _context.Schools.Select(s => new SelectListItem
        {
            Value = s.SchoolId.ToString(),
            Text = s.Name
        }).ToListAsync();
        model.AvailableFoodTypes = await GetAvailableFoodTypes();

        return View("CreateOrEdit", model);
    }

    // GET: Students/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var parentId = _userManager.GetUserId(User);
        var student = await _context.Students
            .Include(s => s.Grade)
            .Include(s => s.School)
            .FirstOrDefaultAsync(m => m.Id == id && m.ParentId == parentId);

        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    // POST: Students/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var parentId = _userManager.GetUserId(User);
        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.Id == id && s.ParentId == parentId);

        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool StudentExists(int id)
    {
        return _context.Students.Any(e => e.Id == id);
    }
    private async Task<List<FoodTypeOption>> GetAvailableFoodTypes()
    {
        return await _context.FoodTypes
            .Select(ft => new FoodTypeOption
            {
                Id = ft.FoodTypeId,
                Name = ft.Name!})
            .ToListAsync();
    }
}